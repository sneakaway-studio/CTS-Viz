using System;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

/**
 *  Change values of a Light2D over time
 */

public class TimeLerp_Light2D : MonoBehaviour
{

    public Light2D light2D;
    public TimeClock timeClock;

    [Header("Time Status")]

    [Tooltip("Indexer to track progression")]
    public TimeTools.Indexer indexer;

    [Tooltip("Total seconds in real time (for the game to progress 24 hours)")]
    public float realSeconds;

    [Tooltip("Number of seconds to display values in each index")]
    public float realSecondsPerTimeProp;
    // e.g. if light2DSettings.Count = 12 / 60 seconds (game is 1 min of realTime) then each index = 5 seconds

    [Tooltip("Current (real time) seconds elapsed")]
    public float realSecondsPassed;

    [Tooltip("Current index")]
    public int currentTimePropsIndex;

    [Tooltip("Is the lerp in progress")]
    public bool changePropsRunning = false;



    [Tooltip("List of light2DSettings to transition / Lerp")]
    public List<ColorTools.Light2DProperties> timeProps;



    void Awake()
    {
        if (light2D == null) light2D = GetComponent<Light2D>();
        if (timeProps.Count <= 0) return;

        indexer = new TimeTools.Indexer(timeProps.Count);

        UpdateTime();
        ChangeProps();
    }

    void Update()
    {
        if (timeProps.Count <= 0) return;

        UpdateTime();

        // should we advance the index?
        if (indexer.current != currentTimePropsIndex)
        {
            // go to next index
            //indexer.NextIndex();



            ChangeProps();
        }
    }

    private void UpdateTime()
    {
        // update the current real seconds
        realSeconds = (float)timeClock.clock.realSeconds;
        realSecondsPerTimeProp = realSeconds / timeProps.Count;
        //Debug.Log($"realSeconds = {realSeconds}");
        //Debug.Log($"timeProps.Count = {timeProps.Count}");

        // get current seconds elapsed from the clock
        realSecondsPassed = (float)timeClock.clock.realSecondsPassed;

        // get the index where we should be
        currentTimePropsIndex = (int)Mathf.Round(realSecondsPassed / realSecondsPerTimeProp);
    }

    void ChangeProps()
    {
        if (changePropsRunning) return;
        changePropsRunning = true;

        indexer.UpdateIndexes(currentTimePropsIndex);

        Debug.Log($"ChangeProps() index: {indexer.current}");
        // start coroutine
        StartCoroutine(ChangePropsCo(timeProps[indexer.current], timeProps[indexer.next], realSecondsPerTimeProp));
    }

    public IEnumerator ChangePropsCo(ColorTools.Light2DProperties props1, ColorTools.Light2DProperties props2, float duration)
    {
        if (changePropsRunning) yield return null;
        changePropsRunning = true;

        // normalized time from props1 > props2
        float t = 0;

        // start at zero, end at 1
        while (t <= 1.01)
        {
            // a way to compare colors
            //if (ColorTools.AreEqual(light2D.color, timeProps[indexer.next].color)){}
            // color 
            light2D.color = Color.Lerp(props1.color, props2.color, t);
            // intensity
            light2D.intensity = Mathf.Lerp(props1.intensity, props2.intensity, t);
            // go to next % of duration
            t += Time.deltaTime / duration;
            yield return null;
        }
        Debug.Log($"ChangePropsCo() index: {indexer.current} FINISHED - now should be {props2.color} and {props2.intensity}");

        changePropsRunning = false;
    }



}


using System;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using System.Collections;

/**
 *  Change property values over time
 */

public class TimeLerpController : MonoBehaviour
{
    [Header("Time Settings")]

    [Tooltip("Reference to timeClock in the game")]
    public TimeClock timeClock;
    public FrameClock frameClock;

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

    [Tooltip("The script that determines *what* properties are lerped")]
    public TimeLerp timeLerpScript;


    private void OnValidate()
    {
        if (frameClock == null) GameObject.Find("FrameClock").GetComponent<FrameClock>();
    }


    void Awake() => Init();

    private void Init()
    {
        if (timeLerpScript.GetTimePropsCount() <= 0) return;
        indexer = new TimeTools.Indexer(timeLerpScript.GetTimePropsCount());
        UpdateTimeFromClock();
        ChangeTimeProp(currentTimePropsIndex);
    }

    void Update() => OnUpdate();

    private void OnUpdate()
    {
        if (timeLerpScript.GetTimePropsCount() <= 0) return;

        UpdateTimeFromClock();

        // should we advance the index?
        if (indexer.current != currentTimePropsIndex)
        {
            ChangeTimeProp(currentTimePropsIndex);
        }
    }

    public float frameClockSecondsPerTimeProp;
    public int frameClockSecondsPassed;
    public int frameClockCurrentTimePropsIndex;

    public float timePropsCurrentPercent;


    private void UpdateTimeFromClock()
    {
        // update (in case they changed) current real seconds and seconds for each prop
        realSeconds = (float)timeClock.clock.realSeconds;
        realSecondsPerTimeProp = realSeconds / timeLerpScript.GetTimePropsCount();

        // update current real seconds elapsed from the clock
        realSecondsPassed = (float)timeClock.clock.realSecondsPassed;

        // update the timeProps index where we should be
        //currentTimePropsIndex = (int)Mathf.Round(realSecondsPassed / realSecondsPerTimeProp);


        frameClockSecondsPerTimeProp = frameClock.framesToSecondsReal / timeLerpScript.GetTimePropsCount();
        frameClockSecondsPassed = frameClock.seconds;
        frameClockCurrentTimePropsIndex = (int)Mathf.Round(frameClockSecondsPassed / frameClockSecondsPerTimeProp);
        currentTimePropsIndex = frameClockCurrentTimePropsIndex;



        // update percent for progress bars, etc.
        timePropsCurrentPercent = (currentTimePropsIndex + .0001f) / indexer.count;


    }

    /**
     *  Calls Coroutine to change new time properties
     */
    void ChangeTimeProp(int newIndex)
    {
        if (changePropsRunning) return;
        changePropsRunning = true;

        indexer.UpdateIndexes(newIndex);

        Debug.Log($"ChangeProps() index: {indexer.current}");
        // start coroutine
        StartCoroutine(ChangePropsCo(realSecondsPerTimeProp));
    }

    public IEnumerator ChangePropsCo(float duration)
    {
        if (changePropsRunning) yield return null;
        changePropsRunning = true;

        // normalized time from props1 > props2
        float t = 0;

        // start at zero, end at 1
        while (t < 1f)
        {
            // lerp props (in child class)
            timeLerpScript.LerpProps(indexer, t);
            // go to next % of duration
            t += Time.deltaTime / duration;
            yield return null;
        }
        // allow next change
        changePropsRunning = false;
        Debug.Log($"ChangePropsCo() index: {indexer.current} FINISHED");
    }


}


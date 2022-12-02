using System;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

public class ColorLerp_Light2D : MonoBehaviour
{
    [Tooltip("List of colors to transition. First color is starting color, duration is how long to transition to next")]
    public List<ColorTools.ColorTransition> colors;

    [Tooltip("Current color")]
    public TimeTools.Indexer indexer;

    [Tooltip("Light on this game object")]
    public ColorInstance colorInstance;

    public Light2D lightComponent;


    public TimeClock timeClock;

    // colors count = 12, 60 seconds (game is 1 min of realTime) so each index = 5 seconds


    public float realSeconds;
    [Tooltip("Number of seconds assigned to each index")]
    public float secondsPerColorIndex;
    public float realSecondsPassed;
    public int currentColorIndex;
    public float hoursToColorDuration;

    void Awake()
    {
        if (colorInstance == null) colorInstance = GetComponent<ColorInstance>();
        //if (lightComponent == null) lightComponent = GetComponent<Light2D>();
        indexer = new TimeTools.Indexer(colors.Count);
    }
    private void Start()
    {
        Debug.Log(24 / colors.Count);
        ChangeColor();
    }

    void Update()
    {
        realSeconds = (float)timeClock.clock.realSeconds;
        secondsPerColorIndex = realSeconds / colors.Count;
        //Debug.Log($"realSeconds = {realSeconds}");
        //Debug.Log($"colors.Count = {colors.Count}");

        // get current seconds elapsed from the clock
        realSecondsPassed = (float)timeClock.clock.realSecondsPassed;

        //Debug.Log(colorInstance.ToString());
        //Debug.Log(lightComponent.color.ToString() + "," + colors[indexer.next].color.ToString());

        // if self-managing own time
        //if (ColorTools.AreEqual(lightComponent.color, colors[indexer.next].color))
        //{
        //    ChangeColor();
        //}




        // get the index where we should be
        currentColorIndex = (int)Mathf.Round(realSecondsPassed / secondsPerColorIndex);
        //// get the scaled duration for this transition
        //hoursToColorDuration = (float)(colors[indexer.current].duration * secondsPerColorIndex);

        if (indexer.current != currentColorIndex)
        {
            indexer.current = currentColorIndex;
            ChangeColor();
        }

        //if (timeClock.clock.gameTime.Hour != currentHour)
        //{
        //    if (hoursToColorIndex < indexer.current)
        //    {

        //    }

        //}

        // always update 
        lightComponent.color = colorInstance.color;
    }

    void ChangeColor()
    {
        Debug.Log("ChangeColor()");
        indexer.NextIndex();
        StartCoroutine(ChangeColorCo(colors[indexer.current].color, colors[indexer.next].color, secondsPerColorIndex));

    }




    public IEnumerator ChangeColorCo(Color color1, Color color2, float duration)
    {
        //Debug.Log(colorInstance.ToString() + "," + color1.ToString() + "," + color2.ToString());
        float t = 0;
        // start at zero, end at 1
        while (t <= 1.01)
        {
            lightComponent.color = Color.Lerp(color1, color2, t);
            colorInstance.color = lightComponent.color;
            t += Time.deltaTime / duration;
            yield return null;
        }
        //Debug.Log("ChangeMaterialColorCo() finished");

    }



}


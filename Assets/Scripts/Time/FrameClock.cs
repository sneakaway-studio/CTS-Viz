using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using TMPro;
using System;

/**
 *  A "Clock" based on frames
 * 
 */

public class FrameClock : MonoBehaviour
{
    public VizManager vizManager;
    public TimeLerpController timeLerpControllerScript;
    // debuggging
    public TMP_Text frameClockTime;
    public TMP_Text frameClockGameTime;
    public TMP_Text realClockTime;
    // display
    public TMP_Text gameTime;
    public TMP_Text frameCount;


    [Tooltip("Frames elapsed (real)")]
    public int frames = 0;


    [Header("Real (kind of) Time")]

    [Tooltip("Number of frames that equal 1 second (real")]
    public int framesToSecondsReal = 60;
    [Tooltip("Total seconds elapsed (real)")]
    public int secondsTotal = 0;
    [Tooltip("Seconds elapsed (real)")]
    public int seconds = 0;
    [Tooltip("Minutes elapsed (real)")]
    public int minutes = 0;
    [Tooltip("Hours elapsed (real)")]
    public int hours = 0;
    [Tooltip("Days elapsed (real)")]
    public int days = 0;
    [Tooltip("Time as a string (real)")]
    public string effectiveTime = "00:00:00";

    [Header("Game Time")]

    [Tooltip("Number of frames that equal 1 second (game")]
    public int framesToSecondsGame = 10;

    [Tooltip("Total seconds elapsed (real)")]
    public int gameSecondsTotal = 0;
    [Tooltip("Seconds elapsed (game)")]
    public float gameSeconds = 0;
    [Tooltip("Minutes elapsed (game)")]
    public float gameMinutes = 0;
    [Tooltip("Hours elapsed (game)")]
    public float gameHours = 0;
    [Tooltip("Days elapsed (game)")]
    public float gameDays = 0;

    [Tooltip("Time as a string (game)")]
    public string effectiveGameTime = "00:00:00";
    public string effectiveGameTimeRelative = "00:00:00";


    [Header("Game Comparisons")]
    public float gameToRealScale = 0;




    public float gamePercentPassed;
    public float gamePercentPassedSeconds;

    private void OnValidate()
    {
        if (vizManager == null) vizManager = GameObject.Find("VizManager").GetComponent<VizManager>();

        // cast one of the ints as a float to get a float back
        gameToRealScale = (float)framesToSecondsGame / framesToSecondsReal;


    }

    // To disable in inspector
    void Start() { }

    //void Update() => UpdateTime();
    void FixedUpdate() => UpdateTime();

    void UpdateTime()
    {
        // always update frames (everything else dirives from this)
        frames++;

        // REAL

        // e.g. 60 / 60 = 0
        if (frames % framesToSecondsReal == 0)
        {
            secondsTotal++;
            seconds++;
        }
        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }
        if (hours >= 24)
        {
            hours = 0;
            days++;
        }
        effectiveTime = $"{hours.FormatZeros("00")}:{minutes.FormatZeros("00")}:{seconds.FormatZeros("00")}";



        if (frames % framesToSecondsGame == 0)
        {
            gameSecondsTotal++;
            gameSeconds++;
        }
        if (gameSeconds >= 60)
        {
            gameSeconds = 0;
            gameMinutes++;
        }
        if (gameMinutes >= 60)
        {
            gameMinutes = 0;
            gameHours++;
        }
        if (gameHours >= 24)
        {
            gameHours = 0;
            gameDays++;
        }
        effectiveGameTime = $"{gameHours.FormatZeros("00")}:{gameMinutes.FormatZeros("00")}:{gameSeconds.FormatZeros("00")}";


        // set % passed using seconds passed of total
        //gamePercentPassed = timeLerpControllerScript.timePropsCurrentPercent;
        gamePercentPassed = Mathf.Lerp(gamePercentPassed, timeLerpControllerScript.timePropsCurrentPercent, Time.deltaTime);
        gamePercentPassedSeconds = Mathf.Lerp(0, 86400, gamePercentPassed);


        frameClockTime.text = effectiveTime + " FrameClockTime";
        frameClockGameTime.text = effectiveGameTime + " FrameClockGame";
        //realClockTime.text = effectiveGameTime + " FrameClockGame";

        //gameTime.text = gamePercentPassedSeconds + " seconds";
        gameTime.text = TimeSpan.FromSeconds(gamePercentPassedSeconds).ToString(@"hh\:mm\:ss");
        frameCount.text = frames + " frames";
    }


    public DateTime StringToDateTime(string timeStr, string format = "HH:mm:ss tt")
    {
        var result = Convert.ToDateTime(timeStr);
        //string test = result.ToString(format, CultureInfo.CurrentCulture);
        return result;
    }
    public TimeSpan StringToTimeSpan(string str)
    {
        TimeSpan ts;

        if (!TimeSpan.TryParse(str, out ts))
        {
            ts = new TimeSpan(0, 1, 0);
        }
        return ts;
    }



}

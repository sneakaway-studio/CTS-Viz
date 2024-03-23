using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using TMPro;
using System;

/// FrameClock
/// - A "Clock" based on frames, not time
/// - Tracking time in game world w/o being bound to real time so we can:
///     - pause game (e.g. it will work right when played again)
///     - render video (e.g. creating 4k requires slow output)
///     - offset properly (because we are in control of time)
/// - Notes:
///                                                            1 tick (frame)
///                                        1 second  =        50 ticks
///                       1 minute  =     60 seconds =      3000 ticks
///          1 hour  =   60 minutes =   3600 seconds =   180,000 ticks 
/// 1 day = 24 hours = 1440 minutes = 86,400 seconds = 4,320,000 ticks 
///                       (60 * 24) = (60 * 60 * 24) = (50 * 60 * 60 * 24) 
///
public class FrameClock : MonoBehaviour
{
    public VizManager vizManager;
    public TimeLerpController timeLerpControllerScript;
    // debuggging
    public GameObject frameClockDebugPanel;
    public TMP_Text frameClockDebugNumbers;
    public TMP_Text frameClockDebugLabels;
    // display
    public GameObject timeDisplayPanel;
    public TMP_Text gameTimeText;
    public TMP_Text frameCount;
    public GameObject progressBar;

    [Header("Settings")]

    public bool singleDay = true;
    public float gameToRealScale = 0;
    public int frames = 0; // Frames elapsed (real) - everything derives from this
    public float fpsGameTime;
    public NewClock gameTime;
    public NewClock realTime;

    private void OnValidate()
    {
        if (vizManager == null) vizManager = GameObject.Find("VizManager").GetComponent<VizManager>();

        Restart();
        UpdateDebugDisplay();
    }

    void Restart()
    {
        gameTime = new NewClock();
        realTime = new NewClock();
        gameTime.Init(fpsGameTime);
        realTime.Init(50);
        // cast one of the ints as a float to get a float back
        gameToRealScale = gameTime.framesPerSecond / realTime.framesPerSecond;
    }

    void Start() { }  // To disable in inspector

    // runs 50 times per second (every 0.02 seconds) by default 
    void FixedUpdate() => UpdateTime();
    void UpdateTime()
    {
        frames++; // always update frames
        // everything else derives from frames
        realTime.Tick(frames);
        gameTime.Tick(frames);

        // reset 
        if (singleDay && gameTime.percentPassed > 1)
        {
            frames = 0;
            Restart();
        }
        UpdateDebugDisplay();
    }

    void UpdateDebugDisplay()
    {
        frameClockDebugPanel.SetActive(vizManager.showDebugging);
        timeDisplayPanel.SetActive(vizManager.showDebugging);
        progressBar.SetActive(vizManager.showDebugging);
        if (!vizManager.showDebugging) return;

        frameClockDebugNumbers.text =
            $"{realTime.currentTime} \n" +
            $"{MathTools.Round(realTime.percentPassed * 100, 4).FormatZeros("0.000")}%\n" +
            $"{gameTime.currentTime} \n" +
            $"{MathTools.Round(gameTime.percentPassed * 100, 4).FormatZeros("0.000")}%\n" +
            $"{frames} \n";

        frameClockDebugLabels.text =
            $"currentTime (real) \n" +
            $"Passed (real) \n" +
            $"currentTime (game) \n" +
            $"Passed (game) \n" +
            $"framesPassed (game)";

        gameTimeText.text = gameTime.currentTime;
        frameCount.text = frames.ToString();
    }
}

[System.Serializable]
public class NewClock
{
    //  FPS      FRAMES     SECONDS     MINUTES    HOURS    DAY

    // Real Time (always 50fps -> .02 seconds/frame)
    // 50 fps        3000        50      50/60
    // 50 fps     180,000      3600        60        1     1/24
    // 50 fps   4,320,000    86,400      1440       24     1

    // Game Time
    // 5 fps         3000        5       5/60
    // 5 fps       18,000      360        6    
    // 1 fps           60       60        1     
    // 1 fps         3600     3600       60        1       1/24   
    // 1 fps       86,400   86,400     1440        24       1      
    // .1 fps         360       36       .6                  
    // .1 fps       8,640      864     14.4               




    public float framesPerSecond = 0;  // # frames played over 1 second
    public float framesPerDay = 0;     // # frames played over 1 day

    public float framesPassed = 0;     // Frames elapsed today
    public float secondsPassed = 0;    // Seconds elapsed today
    public float percentPassed = 0;    // % elapsed today

    // TO ADD
    //public int offset = 0;
    //public float percentPassedOffset = 0;    // % elapsed today


    public string currentTime = "00:00:00";

    public Moment total = new Moment();
    public Moment realTimeEstimate = new Moment();

    public void Init(float _framesPerSecond)
    {
        framesPerSecond = _framesPerSecond;
        framesPerDay = framesPerSecond * (60 * 60 * 24);
        // for each that it takes to make 1 day (basically checking my math)
        realTimeEstimate.days = framesPerSecond / 50;
        realTimeEstimate.hours = realTimeEstimate.days * 24;
        realTimeEstimate.minutes = realTimeEstimate.hours * 60;
        realTimeEstimate.seconds = realTimeEstimate.minutes * 60;
    }

    public void Tick(int frames)
    {
        // add tick
        framesPassed++;
        // update elapsed
        secondsPassed = framesPassed / framesPerSecond;
        percentPassed = framesPassed / framesPerDay;

        // update total time
        total.seconds = Mathf.FloorToInt(secondsPassed);
        total.minutes = Mathf.FloorToInt(total.seconds / 60);
        total.hours = Mathf.FloorToInt(total.seconds / (60 * 60));
        total.days = Mathf.FloorToInt(total.seconds / (60 * 60 * 24));

        currentTime = TimeSpan.FromSeconds(total.seconds).ToString(@"hh\:mm\:ss");
    }
}

/// A moment in time
[System.Serializable]
public class Moment
{
    public float seconds = 0;
    public float minutes = 0;
    public float hours = 0;
    public float days = 0;
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using SneakawayUtilities;

/**
 *  TimeClock
 *  Started with this https://pastebin.com/Mj1K5E64
 *  But then wrote my own https://dotnetfiddle.net/r1Ua6z
 */

public class TimeClock : MonoBehaviour
{
    public VizManager vizManager;
    public RectTransform timeProgressBar;
    public TMP_Text timeText;


    public bool wasPaused = false;
    public DateTime pausedTime;

    [Header("Time Parameters")]

    [TextArea(5, 10)]
    public string realInfo;
    public double timeScale;

    [TextArea(5, 10)]
    public string gameInfo;

    public Clock clock;


    // assign "global" references when Editor compiles code or GO wakes
    private void OnValidate() => AssignReferences();
    private void Awake() => AssignReferences();
    void AssignReferences()
    {
        if (vizManager == null) vizManager = GameObject.Find("VizManager").GetComponent<VizManager>();
    }


    void Start() => Reset();
    public void Reset()
    {
        // create new clock from Scriptable data
        clock = new Clock(vizManager.vizSettings.gameStart, vizManager.vizSettings.realSpan);
    }



    private void Update()
    {
        //ColorTools.ChangeInstanceColor(this, colorInstance, Color.blue, Color.red, 10);
    }

    void FixedUpdate() => RefreshLocalProps();

    void RefreshLocalProps()
    {
        // visualization has been paused
        if (vizManager.isPaused)
        {
            // on the first loop after pause, note the time
            if (!wasPaused) pausedTime = DateTime.Now;
            wasPaused = true;
            return;
        }
        else
        {
            // if the time was paused
            if (wasPaused) Reset();
            wasPaused = false;
        }
        // update clock, then all the values
        clock.UpdateTime();
        // progress bar
        timeProgressBar.anchorMax = new Vector2(((float)clock.gamePercentPassed), 1);
        timeText.text = clock.gameTime.ToString(@"hh\:mm\:ss");

        realInfo = "realStart  \t\t" + clock.realStart.ToString(@"hh\:mm\:ss") + "\n" +
                     "realSpan   \t\t" + clock.realSpan.ToString(@"hh\:mm\:ss") + "\n" +
                     "realTime   \t\t" + clock.realTime.ToString(@"hh\:mm\:ss") + "\n" +
                     "realSpanPassed  \t" + clock.realSpanPassed.ToString(@"hh\:mm\:ss") + "\n" +
                     "realSecondsPassed \t" + clock.realSecondsPassed + "\n" +
                     "realSpan.TotalSeconds \t" + clock.realSpan.TotalSeconds;

        timeScale = clock.timeScale;

        gameInfo = "gameStart \t\t" + clock.gameStart.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameSpan \t\t" + clock.gameSpan.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameTime \t\t" + clock.gameTime.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameSpanPassed \t" + clock.gameSpanPassed.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameSecondsPassed \t" + clock.gameSecondsPassed + "\n" +
                     "gameSpan.TotalSeconds \t" + clock.gameSpan.TotalSeconds + "\n" +
                     "gamePercentPassed \t" + clock.gamePercentPassed + "%\n" +
                     $"hh:mm:ss \t {clock.gameTime.Hour}:{clock.gameTime.Minute}:{clock.gameTime.Second}";


        if (clock.realTime > (clock.realStart + clock.realSpan))
        {
            Reset();
        }
    }



}







/**
 *  A 24 hour clock that 
 *  - keeps track of real and game time
 *  - allows stretching / compressing game time (e.g. 24 game hours can pass in 1 real minute)
 */

[Serializable]
public class Clock
{
    // "real" = device time 
    public DateTime realStart;       // current real time when game starts
    public TimeSpan realSpan;        // real time that spans for 24 hours in the game
    public DateTime realTime;        // current real time
    public TimeSpan realSpanPassed;  // amount of real time passed
    public double realSeconds;       // Total seconds in real time (for the game to progress 24 hours)
    public double realSecondsPassed; // Current (real time) seconds elapsed 

    // "game" = stretched / compressed time in the game
    public DateTime gameStart;       // time to start the game
    public TimeSpan gameSpan;        // always 24 hours
    public DateTime gameTime;        // current time in the game
    public TimeSpan gameSpanPassed;  // game time passed as TimeSpan
    public double gameSeconds;       // current game time in seconds total (see also: gameTime.Second, gameTime.Minute, gameTime.Hour)
    public double gameSecondsPassed; // game time passed in seconds
    public double gamePercentPassed; // game time passed in %

    // this part in progress...
    // https://community-assets.home-assistant.io/original/3X/8/5/8511711857388617d8162dde3031bc7d868535a5.png
    // https://github.com/Moonbase59/studiodisplay/blob/7ca65a3775f96c7e05eb07c74c1bd6e6b9acea9f/python/mqtt-astronomy.py
    // https://github.com/aureldussauge/SunriseSunset
    public enum DayLightEvents
    {
        Midnight, // 00:00:00
        Sunrise_Night,
        Sunrise_Astronomical,
        Sunrise_Nautical,
        Sunrise_BlueHour,
        Sunrise_Civil,
        Sunrise,
        Sunrise_GoldenHour,
        Daylight,
        Morning,
        Noon, // 12:00:00
        Afternoon,
        Sunset_GoldenHour,
        Sunset,
        Sunset_Civil,
        Sunset_BlueHour,
        Sunset_Nautical,
        Sunset_Astronomical,
        Sunset_Night,
    };

    public enum DayPeriods
    {
        Morning, // >= 4 && < 10 - lightIntensity = +=.1f
        Day, // >= 10 && < 14 - lightIntensity = 1f
        Afternoon, // >= 14 && < 20 - lightIntensity = -=.1f
        Night // >= 20 && < 5 - lightIntensity = .1f
    };

    // determines how much game time should be scaled
    public double timeScale;


    /**
     *  Constructors (can take DateTime/TimeSpan or strings)
     */
    public Clock(DateTime _gameStart, TimeSpan _realSpan)
    {
        gameStart = _gameStart;
        realSpan = _realSpan;
        Init();
    }
    public Clock(string _gameStart, string _realSpan)
    {
        gameStart = StringToDateTime(_gameStart);
        //Debug.Log($"gameStart = {gameStart}");
        realSpan = StringToTimeSpan(_realSpan);
        //Debug.Log($"realSpan = {realSpan}");
        Init();
    }

    /**
     *  Initialize others props
     */
    void Init()
    {
        realStart = DateTime.Now; // DateTime.UtcNow;
        //Debug.Log($"realStart = {realStart}");

        realSeconds = realSpan.TotalSeconds;
        gameSeconds = gameSpan.TotalSeconds;

        gameSpan = new TimeSpan(23, 59, 00);
        //Debug.Log($"gameSpan = {gameSpan}");

        // Create a timeScale representing the factor between total seconds
        timeScale = gameSpan.TotalSeconds / realSpan.TotalSeconds;
        //Debug.Log($"gameSpan.TotalSeconds = {gameSpan.TotalSeconds}");
        //Debug.Log($"realSpan.TotalSeconds = {realSpan.TotalSeconds}");
        //Debug.Log($"timeScale = {timeScale}");
    }


    /**
     *  Track ("real") time to compute elapsed game time 
     */
    public DateTime UpdateTime()
    {

        // 1. Update current real time, get the seconds passed

        // update current real time
        realTime = DateTime.Now;

        // seconds since real time started
        realSpanPassed = (realTime - realStart).Duration();
        //Debug.Log($"realSpanPassed = {realSpanPassed.ToString(@"hh\:mm\:ss")}");

        realSecondsPassed = realSpanPassed.TotalSeconds;
        //Debug.Log($"realSecondsPassed = {realSecondsPassed}");




        // 2. Find the seconds that should have passed in the game

        gameSecondsPassed = realSecondsPassed * timeScale;
        //Debug.Log($"gameSecondsPassed = realSecondsPassed * timeScale = {gameSecondsPassed} = {realSecondsPassed} * {timeScale}");

        // set span passed using seconds passed 
        gameSpanPassed = TimeSpan.FromSeconds(gameSecondsPassed);
        //Debug.Log(TimeSpan.FromSeconds(realSecondsPassed / timeScale));
        //Debug.Log($"gameSpanPassed = {gameSpanPassed}");

        // set % passed using seconds passed of total
        gamePercentPassed = gameSecondsPassed / gameSpan.TotalSeconds;

        // set current game time by adding seconds seconds to start
        gameTime = gameStart.AddSeconds(gameSecondsPassed);


        //hours = gameSecondsPassed / 60 / 60;
        //minutes = (hours % 1) * 60;
        //seconds = (minutes % 1) * 60;
        //gameTime = new TimeSpan((int)hours, (int)minutes, (int)seconds);


        return realTime;
    }

    public DateTime StringToDateTime(string timeStr, string format = "hh:mm:ss tt")
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
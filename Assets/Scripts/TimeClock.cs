using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

/**
 *  TimeClock
 *  Started with this https://pastebin.com/Mj1K5E64
 *  But then wrote my own https://dotnetfiddle.net/r1Ua6z
 */

public class TimeClock : MonoBehaviour
{
    public Visualization visualization;
    public RectTransform timeViz;
    public TMP_Text timeText;

    public Light2D light2d;
    public float lightIntensity = 1f;

    public bool wasPaused = false;
    public DateTime pausedTime;

    [Header("Time Parameters")]

    [Tooltip("Reference to vizSettings")]
    public VizSettings vizSettingsObj;

    [TextArea(5, 10)]
    public string realInfo;
    public double timeScale;

    [TextArea(5, 10)]
    public string gameInfo;

    public Clock clock;


    void Start() => Reset();

    public void Reset()
    {
        // create new clock from Scriptable data
        clock = new Clock(vizSettingsObj.gameStart, vizSettingsObj.realSpan);
    }

    void FixedUpdate() => RefreshLocalProps();

    void RefreshLocalProps()
    {
        // visualization has been paused
        if (visualization.isPaused)
        {
            // on the first loop after pause, note the time
            if (!wasPaused) pausedTime = DateTime.Now;
            wasPaused = true;
            return;
        }
        else
        {
            // if the time was paused
            if (wasPaused)
            {
                Reset();
            }
            wasPaused = false;
        }
        // update clock, then all the values
        clock.UpdateTime();

        timeViz.anchoredPosition = new Vector2(((float)clock.gamePercentPassed * Screen.width) - (Screen.width / 2), 20);
        timeText.text = clock.gameTime.ToString(@"hh\:mm\:ss");

        realInfo = "realStart  \\tt" + clock.realStart.ToString(@"hh\:mm\:ss") + "\n" +
                     "realSpan   \t\t" + clock.realSpan.ToString(@"hh\:mm\:ss") + "\n" +
                     "realTime   \t\t" + clock.realTime.ToString(@"hh\:mm\:ss") + "\n" +
                     "realSpanPassed  \t" + clock.realSpanPassed.ToString(@"hh\:mm\:ss") + "\n" +
                     "realSecondsPassed \t" + clock.realSecondsPassed;

        timeScale = clock.timeScale;

        gameInfo = "gameStart \t\t" + clock.gameStart.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameSpan \t\t" + clock.gameSpan.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameTime \t\t" + clock.gameTime.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameSpanPassed \t" + clock.gameSpanPassed.ToString(@"hh\:mm\:ss") + "\n" +
                     "gameSecondsPassed \t" + clock.gameSecondsPassed + "\n" +
                     "gamePercentPassed \t" + clock.gamePercentPassed + "%\n" +
                     $"hh:mm:ss \t {clock.hours}:{clock.minutes}:{clock.seconds}";

        if (clock.gameTime.Hour >= 4 && clock.gameTime.Hour < 10)
        {
            lightIntensity += .013f;
            lightIntensity = Mathf.Min(lightIntensity, 1);
            Debug.Log($"clock.gameTime.Hour = {clock.gameTime.Hour} - MORNING");
        }
        else if (clock.gameTime.Hour >= 10 && clock.gameTime.Hour < 14)
        {
            lightIntensity = 1;
            Debug.Log($"clock.gameTime.Hour = {clock.gameTime.Hour} - DAY");
        }
        else if (clock.gameTime.Hour >= 14 && clock.gameTime.Hour < 22)
        {
            lightIntensity -= .008f;
            lightIntensity = Mathf.Max(lightIntensity, .1f);
            Debug.Log($"clock.gameTime.Hour = {clock.gameTime.Hour} - AFTERNOON");
        }
        else
        //if (clock.gameTime.Hour >= 20 && clock.gameTime.Hour < 5)
        {
            lightIntensity = .1f;
            Debug.Log($"clock.gameTime.Hour = {clock.gameTime.Hour} - NIGHT");
        }

        light2d.intensity = lightIntensity;

        if (clock.realTime > (clock.realStart + clock.realSpan))
        {
            Reset();
        }
    }
}


[Serializable]
public class Clock
{
    // real world time
    public DateTime realStart; // always the current time
    public TimeSpan realSpan;
    public DateTime realTime;
    public TimeSpan realSpanPassed;
    public double realSecondsPassed;
    // game
    public DateTime gameStart;
    public TimeSpan gameSpan; // always 24 hours
    public DateTime gameTime;
    public TimeSpan gameSpanPassed;
    public double gameSecondsPassed;
    public double gamePercentPassed;

    public double hours;
    public double minutes;
    public double seconds;

    // use to get the new value
    public double timeScale;


    /**
     *  Constructor
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
        Debug.Log($"gameStart = {gameStart}");
        realSpan = StringToTimeSpan(_realSpan);
        Debug.Log($"realSpan = {realSpan}");
        Init();
    }

    /**
     *  Then initialize others props
     */
    void Init()
    {
        realStart = DateTime.Now; // DateTime.UtcNow;
        Debug.Log($"realStart = {realStart}");

        gameSpan = new TimeSpan(23, 59, 59);
        Debug.Log($"gameSpan = {gameSpan}");

        // Create a timeScale representing the factor between total seconds
        timeScale = gameSpan.TotalSeconds / realSpan.TotalSeconds;
        Debug.Log($"gameSpan.TotalSeconds = {gameSpan.TotalSeconds}");
        Debug.Log($"realSpan.TotalSeconds = {realSpan.TotalSeconds}");
        Debug.Log($"timeScale = {timeScale}");
    }


    /**
     *  Track ("real") time to compute elapsed game time 
     */
    public DateTime UpdateTime()
    {

        // 1. Update current real time, get the seconds passed

        // update current real time
        realTime = DateTime.Now;

        // seconds since real start
        realSpanPassed = (realTime - realStart).Duration();
        //Debug.Log(realSpanPassed.ToString(@"hh\:mm\:ss"));

        realSecondsPassed = realSpanPassed.TotalSeconds;
        //Debug.Log($"realSecondsPassed = {realSecondsPassed}");




        // 2. Find the seconds that should have passed in the game

        gameSecondsPassed = realSecondsPassed * timeScale;
        //Debug.Log($"gameSecondsPassed = realSecondsPassed * timeScale = {gameSecondsPassed} = {realSecondsPassed} * {timeScale}");

        gameSpanPassed = TimeSpan.FromSeconds(gameSecondsPassed);
        //Debug.Log(TimeSpan.FromSeconds(realSecondsPassed / timeScale));
        //Debug.Log($"gameSpanPassed = {gameSpanPassed}");

        //gameSecondsPassed = gameSpanPassed.TotalSeconds;
        //Debug.Log($"gameSecondsPassed = {gameSecondsPassed}");



        gameTime = gameStart.AddSeconds(gameSecondsPassed);


        gamePercentPassed = gameSecondsPassed / gameSpan.TotalSeconds;


        //hours = realNow.TimeOfDay.TotalMinutes % minutesPerDay;
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
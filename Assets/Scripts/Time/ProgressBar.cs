using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public RectTransform timeProgressBar;
    public FrameClock frameClock;

    public TimeLerpController timeLerpController;


    private void OnValidate()
    {
        if (timeProgressBar == null) timeProgressBar = GetComponentInChildren<RectTransform>();
    }

    private void FixedUpdate()
    {
        // progress bar
        //timeProgressBar.anchorMax = new Vector2(((float)frameClock.gamePercentPassed), 1);
        //timeProgressBar.anchorMax = new Vector2(((float)timeLerpController.timePropsCurrentPercent), 1);

        // both seem to work
        //timeProgressBar.anchorMax = new Vector2(Mathf.Lerp(timeProgressBar.anchorMax.x, frameClock.gameTime.percentPassed, Time.deltaTime), 1);
        timeProgressBar.anchorMax = new Vector2(frameClock.gameTime.percentPassed, 1);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;

/**
 *  Animate a gradient using a list of colors - TimeLerp should replace this...
 */

public class ShaderVars_Color_GradientAnimation : ShaderVars
{
    [Tooltip("List of colors to cycle thorugh")]
    public List<Color> colors = new List<Color>();

    [Tooltip("Generate random colors (good for testing) based on length of colors list")]
    public bool useRandom = true;

    [Tooltip("List of colors from TimeProps")]
    public TimeProps_Light2D timePropsSettings;



    [Tooltip("Time between lerping 0-1")]
    public float time = 0;

    [Tooltip("Amount to increase on each fixed update")]
    public float tick = .01f;




    private void Start()
    {
        if (timePropsSettings != null) ResetColorsFromTimeProps();
    }

    // use TimeProps instead of the colors here
    void ResetColorsFromTimeProps()
    {
        colors = new List<Color>();
        foreach (TimeProperties_Light2D prop in timePropsSettings.list)
        {
            colors.Add(prop.color);
        }
    }


    private void FixedUpdate()
    {
        // FixedUpdate adds ticks corresponding to framerate
        time += tick;
    }

    private void Update()
    {
        if (time > 1)
        {
            ChangeColors(); // change colors
            time = 0; // reset
        }
        UpdateShader();
    }

    public override void UpdateShader()
    {
        // Inherits reference to material from parent
        material.SetColor("_Color_A", Color.Lerp(colors[0], colors[1], time));
        material.SetColor("_Color_B", Color.Lerp(colors[1], colors[2], time));
    }

    void ChangeColors()
    {
        // loop through the colors list
        for (int i = 0; i < colors.Count; i++)
        {
            if (i < colors.Count - 1)
                // shift index of each value by -1 
                colors[i] = colors[i + 1];
            else
            {
                if (useRandom) // add a new color
                    colors[i] = ColorTools.RandomColor();
                else // move last color to first
                    colors[i] = colors[0];
            }

        }
    }


}

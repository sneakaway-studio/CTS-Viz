using System;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using UnityEngine.Rendering.Universal;
using System.Collections;

/**
 *  Change property values over time - Light2D version
 */

public class TimeLerp_Light2D : TimeLerp
{

    [Header("Property Settings")]

    [Tooltip("Component to change")]
    public Light2D light2D;

    //[Tooltip("List of properties to transition / Lerp")]
    //public List<TimeProperties_Light2D> timeProps = new List<TimeProperties_Light2D>();


    public TimeProps_Light2D timePropsSettings;

    public override TimeProperties GetTimeProp(int index)
    {
        return timePropsSettings.list[index];
    }

    public override int GetTimePropsCount()
    {
        return timePropsSettings.list.Count;
    }

    public override void LerpProps(TimeTools.Indexer indexer, float t)
    {
        TimeProperties_Light2D props1 = timePropsSettings.list[indexer.current];
        TimeProperties_Light2D props2 = timePropsSettings.list[indexer.next];

        light2D.color = Color.Lerp(props1.color, props2.color, t);
        light2D.intensity = Mathf.Lerp(props1.intensity, props2.intensity, t);
    }

    void Awake()
    {
        if (light2D == null) light2D = GetComponent<Light2D>();
    }




}


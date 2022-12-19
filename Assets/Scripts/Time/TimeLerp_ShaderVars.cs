using System;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.UI;

/**
 *  Change property values over time - ShaderVars version
 */

public class TimeLerp_ShaderVars : TimeLerp
{

    [Header("Property Settings")]

    [Tooltip("Component to change")]
    public Material material;


    [Tooltip("List of properties to transition / Lerp")]
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
        TimeProperties_Light2D props1 = timePropsSettings.list[indexer.prev];
        TimeProperties_Light2D props2 = timePropsSettings.list[indexer.current];
        TimeProperties_Light2D props3 = timePropsSettings.list[indexer.next];

        material.SetColor("_Color_A", Color.Lerp(props1.color, props2.color, t));
        material.SetColor("_Color_B", Color.Lerp(props2.color, props3.color, t));

    }

    private void OnValidate()
    {
        if (material == null) material = GetComponent<RawImage>().material;
    }



}


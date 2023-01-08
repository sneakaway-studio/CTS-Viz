using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using System;

/**
 *  Change property values over time - BASE CLASS
 *  - methods declared abstract to require child classes to (re)implement
 */

public abstract class TimeLerp : MonoBehaviour
{
    public int timePropsCount;

    // return a single property
    public abstract TimeProperties GetTimeProp(int index);
    // return the length of the list
    public abstract int GetTimePropsCount();
    // call the function to lerp to next prop
    public abstract void LerpProps(TimeTools.Indexer indexer, float t);

    private void OnValidate()
    {
        timePropsCount = GetTimePropsCount();
    }

}



/**
 *  Define the classes for the different TimeProperties types
 */

[System.Serializable]
public class TimeProperties { }

[System.Serializable]
public class TimeProperties_Light2D : TimeProperties
{
    public Color color;
    public float intensity;
}

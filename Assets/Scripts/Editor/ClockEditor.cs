using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
 *  Create buttons
 */

[CustomEditor(typeof(TimeClock))]
public class ClockEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TimeClock script = (TimeClock)target;




        if (GUILayout.Button("Reset TimeClock"))
        {
            script.Reset();
        }




    }
}
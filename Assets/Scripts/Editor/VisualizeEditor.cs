using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
 *  Create Visualize control buttons
 */

[CustomEditor(typeof(VizManager))]
public class VisualizeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VizManager visualization = (VizManager)target;

        if (GUILayout.Button("Update Visualization"))
        {
            visualization.Run();
        }

        if (GUILayout.Button("Clear Visualization"))
        {
            visualization.Clear();
        }


    }
}
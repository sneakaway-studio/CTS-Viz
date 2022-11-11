using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
 *  Create Visualize control buttons
 */

[CustomEditor(typeof(Visualization))]
public class VisualizeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Visualization visualization = (Visualization)target;

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
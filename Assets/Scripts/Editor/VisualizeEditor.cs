using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
 *  Create Visualize control buttons
 */

[CustomEditor(typeof(Visualize))]
public class VisualizeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Visualize visualize = (Visualize)target;

        if (GUILayout.Button("Update Visualization"))
        {
            visualize.Run();
        }

        if (GUILayout.Button("Clear Visualization"))
        {
            visualize.Clear();
        }


    }
}
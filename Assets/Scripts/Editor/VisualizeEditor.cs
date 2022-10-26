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

        Visualize script = (Visualize)target;

        if (GUILayout.Button("Update Visualization"))
        {
            script.Run();
        }

        if (GUILayout.Button("Clear Visualization"))
        {
            script.Clear();
        }


    }
}
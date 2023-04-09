using UnityEngine;
using System.Collections;

/**
 *  Test with multiple displays 
 *  https://docs.unity3d.com/Manual/MultiDisplay.html
 *  1. Enable script on MainCamera
 *  2. Enable other cameras
 */

public class ActivateAllDisplays : MonoBehaviour
{
    public Camera[] cams;


    void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        //Call function when new display is connected
        Display.onDisplaysUpdated += OnDisplaysUpdated;


        for (int i = 0; i < cams.Length; i++)
        {
            cams[i].targetDisplay = i;
        }

    }

    void OnDisplaysUpdated()
    {
        Debug.Log("New Display Connected. Show Display Option Menu....");
    }

}

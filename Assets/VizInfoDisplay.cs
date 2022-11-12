using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 *  So we know what &%@! Scriptable is loaded
 */

public class VizInfoDisplay : MonoBehaviour
{
    Visualization visualization;
    GameObject infoCanvas;
    TMP_Text infoText;
    string infoTextStr = "";

    void Start()
    {
        infoCanvas.SetActive(false);
    }

    private void OnValidate()
    {
        if (visualization == null)
            visualization = gameObject.GetComponent<Visualization>();
        if (infoCanvas == null)
            infoCanvas = GameObject.Find("InfoCanvas");
        if (infoText == null)
            infoText = infoCanvas.GetComponentInChildren<TMP_Text>();
        UpdateInfoText();
    }

    void UpdateInfoText()
    {
        if (infoText == null) return;
        // add text
        infoTextStr = $"VizSettings = {visualization.vizSettings.name}";
        // update obj 
        infoText.text = infoTextStr;
        // make sure its visible
        infoCanvas.SetActive(true);
    }

}

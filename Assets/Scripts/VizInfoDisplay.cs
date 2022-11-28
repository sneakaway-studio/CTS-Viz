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
    GameObject infoPanel;
    TMP_Text infoText;
    string infoTextStr = "";

    void Start()
    {
        infoPanel.SetActive(false);
    }

    private void OnValidate()
    {
        if (visualization == null)
            visualization = gameObject.GetComponent<Visualization>();
        if (infoPanel == null)
            infoPanel = GameObject.Find("InfoPanel");
        if (infoText == null)
            infoText = infoPanel.GetComponentInChildren<TMP_Text>();
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
        infoPanel.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 *  So we know what &%@! Scriptable is loaded
 */

public class UI_DevInfo : MonoBehaviour
{
    public Visualization visualization;
    public GameObject infoPanel;
    public TMP_Text infoText;

    void Start()
    {
        // we don't actually need this when playing
        infoPanel.SetActive(false);
    }

    /**
     *  Run when Unity compiles code (in Editor)
     */
    private void OnValidate()
    {
        if (visualization == null)
            visualization = GameObject.Find("Visualization").GetComponent<Visualization>();
        if (infoPanel == null)
            infoPanel = GameObject.Find("Canvas_DevInfo");
        if (infoText == null)
            infoText = infoPanel.GetComponentInChildren<TMP_Text>();
        UpdateInfoText();
    }

    void UpdateInfoText()
    {
        if (infoText == null) return;
        // the settings we are using
        infoText.text = $"current VizSettings: {visualization.vizSettings.name}";
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 *  So we know what &%@! Scriptable is loaded
 */

public class UI_DevInfo : MonoBehaviour
{
    public VizManager vizManager;
    public GameObject infoPanel;
    public TMP_Text infoText;

    // assign "global" references when Editor compiles code or GO wakes
    private void OnValidate() => AssignReferences();
    private void Awake() => AssignReferences();
    void AssignReferences()
    {
        if (vizManager == null) vizManager = GameObject.Find("VizManager").GetComponent<VizManager>();
        if (infoPanel == null) infoPanel = GameObject.Find("Canvas_DevInfo");
        if (infoText == null) infoText = infoPanel.GetComponentInChildren<TMP_Text>();
        UpdateInfoText();
    }



    void Start()
    {
        // we don't actually need this when playing
        infoPanel.SetActive(false);
    }

    void UpdateInfoText()
    {
        if (infoText == null) return;
        // the settings we are using
        infoText.text = $"current VizSettings: {vizManager.vizSettings.name}";
    }

}

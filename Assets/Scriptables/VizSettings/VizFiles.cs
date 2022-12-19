using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class to encapsulate files and settings (for those files) for visualization
 */


[System.Serializable]
public class VizFiles
{

    [Header("These fields define SpriteFolder (below)")]
    [Space]

    [Tooltip("UTC 24 & offset")]
    public string directory = "";

    [Tooltip("Number of this offset")]
    public string num = "01";

    [Tooltip("Type (house | plant)")]
    public string type;

    [Tooltip("Subtype (leaves | stem | whatever)")]
    public string subtype;

    [Space]

    [Tooltip("{directory}/{num}/{type}/PNG/[{subtype}/]")]
    public string spriteFolder;

    [Space]

    [Tooltip("Files found")]
    public List<Sprite> files = new List<Sprite>();



    [Header("Settings for these files")]
    [Space]

    [Tooltip("Distance from center of Visualization position")]
    public float positionRadius = 5.0f;

    [SerializeField]
    [Tooltip("Range (min/max) for scale")]
    [Range(0, 4)] public float scaleMin = 0.8f;

    [SerializeField]
    [Tooltip("Range (min/max) for scale")]
    [Range(0, 4)] public float scaleMax = 1.2f;

    [Tooltip("Number of these files to use (will repeat after first loop)")]
    [Range(0, 400)] public int max = 50;


}

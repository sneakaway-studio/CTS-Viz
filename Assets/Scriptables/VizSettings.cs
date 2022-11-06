using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VizFiles
{
    [Tooltip("Lock the inspector, then drag in the group")]
    public List<Sprite> filesToUse = new List<Sprite>();

    [Tooltip("24 hour offset")]
    public string offset24 = "00";

    [Tooltip("UTC offset")]
    public string offsetUTC = "00";

    [Tooltip("Number of this offset")]
    public string num = "01";

    public enum TypeHouseOrPlant { house, plant };
    [Tooltip("House or Plant")]
    public TypeHouseOrPlant type = new TypeHouseOrPlant();


    [Tooltip("Distance from center of Visualization position")]
    public float positionRadius = 5.0f;

    [SerializeField]
    [Tooltip("Range (min/max) for scale")]
    [Range(0, 20)] public float scaleMin = 1.0f;
    [Range(0, 20)] public float scaleMax = 1.5f;

    [Tooltip("Number of these files to use (will repeat after first loop)")]
    [Range(0, 200)] public int max = 50;
}

[System.Serializable]
[CreateAssetMenu(fileName = "VizSettings", menuName = "Create VizSettings", order = 1)]
public class VizSettings : ScriptableObject
{


    [Tooltip("List of files")]
    public List<VizFiles> vizFiles = new List<VizFiles>();


    [Header("Time Zones")]







    // Unity cannot serialize DateTime or TimeSpan so these will be converted by TimeClock

    [Tooltip("Time of (game) day to start cycle, format '14:00:00' (2pm UTC)")]
    public string gameStart = "12:00:00";


    [Tooltip("Amount of (real) time to visualize a complete (game) day; format: '00:30:00' (30 minutes to show 1 day)")]
    public string realSpan = "00:10:00";







    [Tooltip("Add anything here")]
    public string notes = "";





    [Header("Animation Settings")]


    [Tooltip("Animation movement duration (higher is slower)")]
    [Range(0, 100)] public float animDirectionDurationMin = 5f;
    [Range(0, 100)] public float animDirectionDurationMax = 15f;

    [Tooltip("Range (min/max) for animation direction speed")]
    [Range(-10f, 10)] public float animDirectionMin = -0.3f;
    [Range(-10f, 10)] public float animDirectionMax = 0.3f;


    [Tooltip("Animation rotation time (higher is slower)")]
    [Range(0, 100)] public float animRotateTime = 30f;

    [Tooltip("Range (min/max) for animation rotation direction")]
    [Range(-10f, 10)] public float animRotateDirectionMin = -0.3f;
    [Range(-10f, 10)] public float animRotateDirectionMax = 0.3f;







    [Header("Gradient")]


    public UnityEngine.Gradient gradient;


    /**
     *  OnValidate()
     *  - called when a component's exposed properties are changed in the inspector
     */
    public void OnValidate()
    {
        //Debug.Log($"OnValidate() {Time.time}");
    }
    public void OnBeforeSerialize()
    {

    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/**
 *  Class to encapsulate files and settings (for those files) for visualization
 */

[System.Serializable]
public class VizFiles
{
    [Header("Location")]

    [Tooltip("UTC 24 & offset")]
    public string directory = "";

    [Tooltip("Number of this offset")]
    public string num = "01";

    [Tooltip("Type (house | plant)")]
    public string type;

    [Tooltip("Subtype (leaves | stem | whatever)")]
    public string subtype;

    [Tooltip("{directory}/{num}/{type}/PNG/[{subtype}/]")]
    public string spriteFolder;

    [Tooltip("Files found")]
    public List<Sprite> files = new List<Sprite>();


    [Header("Settings")]

    [Tooltip("Distance from center of Visualization position")]
    public float positionRadius = 5.0f;

    [SerializeField]
    [Tooltip("Range (min/max) for scale")]
    [Range(0, 4)] public float scaleMin = 1.0f;

    [SerializeField]
    [Tooltip("Range (min/max) for scale")]
    [Range(0, 4)] public float scaleMax = 1.5f;

    [Tooltip("Number of these files to use (will repeat after first loop)")]
    [Range(0, 200)] public int max = 50;
}


[System.Serializable]
[CreateAssetMenu(fileName = "VizSettings", menuName = "Scriptables/VizSettings", order = 1)]
[ExecuteInEditMode]
public class VizSettings : ScriptableObject
{


    [Tooltip("List of files")]
    public List<VizFiles> vizFiles = new List<VizFiles>();


    [Header("Time Settings")]



    // sprite folder in assetPath/
    //spriteFolder = $"{offset24}-00/01/house/PNG/";




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



    static string assetPath = "Resources/UTC-ORIGINALS-PNG/";



    [Header("Gradient")]


    public UnityEngine.Gradient gradient;



    /**
     *  OnValidate()
     *  - called when a component's exposed properties are changed in the inspector
     */
    public void OnValidate()
    {

        foreach (var f in vizFiles)
        {
            //if (assetManager == null)
            //    assetManager = GameObject.FindObjectOfType<Visualization>().GetComponent<AssetManager>();


            //f.folder = f.type1;
            //f.type = f.folder;
            f.spriteFolder = $"{f.directory}/{f.num}/{f.type}/PNG/";
            if (f.subtype != null && f.subtype != "")
                f.spriteFolder += $"{f.subtype}/";


            // update list of files 
            f.files = AssetManagerStatic.GetSpritesAtPath(assetPath, f.spriteFolder).ToList();
        }



        Debug.Log($"OnValidate() {Time.time}");
    }
    public void OnBeforeSerialize()
    {
        Debug.Log($"OnBeforeSerialize() {Time.time}");
    }



    //    private void OnDisable()
    //    {
    //        Debug.Log($"OnDisable() {Time.time}");
    //#if UNITY_EDITOR
    //        UnityEditor.EditorUtility.SetDirty(this);
    //#endif
    //    }





}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/**
 *  Class to encapsulate file references and settings for visualization
 */

[System.Serializable]
[CreateAssetMenu(fileName = "VizSettings", menuName = "Scriptables/VizSettings", order = 1)]
[ExecuteInEditMode]
public class VizSettings : ScriptableObject
{
    [Tooltip("Add anything here")]
    public string notes = "";

    [Tooltip("List of files")]
    public List<VizFiles> vizFilesList = new List<VizFiles>();



    [Header("Time Settings")]

    // sprite folder in assetPath/
    //spriteFolder = $"{offset24}-00/01/house/PNG/";


    // Unity cannot serialize DateTime or TimeSpan so these will be converted by TimeClock

    [Tooltip("Time of (game) day to start cycle, format '14:00:00' (2pm UTC)")]
    public string gameStart = "12:00:00";


    [Tooltip("Amount of (real) time to visualize a complete (game) day; format: '00:30:00' (30 minutes to show 1 day)")]
    public string realSpan = "00:10:00";




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

        foreach (var f in vizFilesList)
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


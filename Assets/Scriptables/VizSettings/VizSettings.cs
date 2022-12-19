using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

/**
 *  Class to encapsulate file references and settings for visualization
 */

[System.Serializable]
[CreateAssetMenu(fileName = "VizSettings", menuName = "Scriptables/VizSettings/", order = 1)]
[ExecuteInEditMode]
public class VizSettings : ScriptableObject
{
    [Tooltip("Add anything here")]
    public string notes = "";

    // Unity cannot serialize DateTime or TimeSpan so these will be converted by TimeClock

    [Tooltip("Time of (game) day to start cycle, format '14:00:00' (2pm UTC)")]
    public string gameStart = "00:00:00";

    [Tooltip("Amount of (real) time to visualize a complete (game) day; format: '00:30:00' (30 minutes to show 1 day)")]
    public string realSpan = "00:01:00";




    [Header("Images")]

    [Tooltip("Number of images total in this file")]
    public int totalImages;

    [Tooltip("List of files")]
    [FormerlySerializedAs("vizFiles")] public List<VizFiles> vizFilesList = new List<VizFiles>();









    static string assetPath = "Resources/UTC-ORIGINALS-PNG/";


    //[Header("Gradient")]
    //public UnityEngine.Gradient gradient;



    /**
     *  OnValidate()
     *  - called when a component's exposed properties are changed in the inspector
     */
    public void OnValidate()
    {
        // sum the total of all max values e.g. [50.50] = 100
        totalImages = vizFilesList.Sum(item => item.max);

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



        //Debug.Log($"OnValidate() {Time.time}");
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


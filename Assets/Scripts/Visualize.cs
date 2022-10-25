using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SneakawayUtilities;
using System;

/**
 *  Visualization controller
 *  - Instantiates all sprites, holds|generates their properties (though they control their state afterwards)
 */

public class Visualize : MonoBehaviour
{

    [Header("Visualization Settings")]

    public VizSettings vizSettings;



    [Tooltip("Prefab for sprite")]
    public GameObject prefab;

    [Tooltip("Create visualization on play")]
    public bool runOnStart = true;

    [Tooltip("Reset visualization after editing properties")]
    public bool resetPropsAfterEdit = true;




    [Header("Image Collections")]

    [Tooltip("Number of images chosen")]
    public int totalImages;

    [Tooltip("Images (and count) selected")]
    public Sprite[] selected;

    [Tooltip("The prefabs")]
    public List<GameObject> prefabs = new List<GameObject>();



    [Header("Animation Settings")]

    [Tooltip("Animate the visualization")]
    public bool animate = true;




    private void Awake()
    {
        if (runOnStart) Run();
    }
    private void Start()
    {
        // to disable in inspector
    }




    //public Sprite[] MergeScriptableArrays()
    //{
    //    Sprite[] total = new Sprite[];
    //    
    //}


    public void Run()
    {
        //Clear();

        // sum the total of all max values e.g. [50.50] = 100
        totalImages = vizSettings.vizFiles.Sum(item => item.max);

        // loop through the vizFile Objects
        foreach (VizFiles vizFilesObj in vizSettings.vizFiles)
        {

            // safety
            // the number of images to select - is set to max to allow
            // more images in the visualization
            //int thisMax = (int)Mathf.Max(vizFilesObj.filesToUse.Count, imageMax);

            //// then shuffle them 
            //selected = SpriteExtensions.Shuffle(selected);

            // loop for number of images to add
            for (int i = 0; i < vizFilesObj.max; i++)
            {
                // loop through them all once...
                int selectedImageIndex = i;
                // then after max is > than count 
                if (i >= vizFilesObj.filesToUse.Count)
                    // choose a random image for the selectedImageIndex
                    selectedImageIndex = UnityEngine.Random.Range(0, vizFilesObj.filesToUse.Count - 1);

                // ^ IOW we don't need a safety or shuffle


                // instantiate game object under this parent
                GameObject g = Instantiate(prefab, transform);
                // set default values
                g.GetComponent<SpritePrefab>().SetProperties(this, vizFilesObj.filesToUse[selectedImageIndex], i, vizSettings, vizFilesObj);
                // add to prefabs list
                prefabs.Add(g);
            }
        }
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            SpritePrefabAnim anim = child.gameObject.GetComponent<SpritePrefabAnim>();
            if (anim.tweener != null) anim.Kill();

            Destroy(child.gameObject);
            prefabs.Clear();
        }
    }



}


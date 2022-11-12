using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SneakawayUtilities;
using UnityEngine.UI.Extensions;

/**
 *  Visualization controller
 *  - Instantiates all sprites
 *  - Generates initial properties (they control their state afterwards)
 */

public class Visualization : MonoBehaviour
{

    [Header("Visualization Settings")]

    public VizSettings vizSettings;

    [Tooltip("Path to all sprite assets")]
    public string assetPath = "Resources/UTC-ORIGINALS-PNG/";















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



    public Gradient2 gradient;
    public Gradient2 gradientNoise;





    private void Awake()
    {
        if (runOnStart) Run();
    }
    private void Start()
    {
        // to disable in inspector
    }






    public void Run()
    {
        //Clear();






        //gradient.EffectGradient.colorKeys[0] = vizSettings.color1;


        gradient.EffectGradient = vizSettings.gradient;
        gradientNoise.EffectGradient = vizSettings.gradient;


        // sum the total of all max values e.g. [50.50] = 100
        totalImages = vizSettings.vizFiles.Sum(item => item.max);

        // loop through the vizFile Objects
        foreach (VizFiles vizFilesObj in vizSettings.vizFiles)
        {
            // loop for number of images to add
            for (int i = 0; i < vizFilesObj.max; i++)
            {
                // loop through them all once...
                int selectedImageIndex = i;
                // then after max is > than count 
                if (i >= vizFilesObj.files.Count)
                    // choose a random image for the selectedImageIndex
                    selectedImageIndex = UnityEngine.Random.Range(0, vizFilesObj.files.Count - 1);

                // ^ IOW we don't need a safety or shuffle


                // instantiate game object under this parent
                GameObject g = Instantiate(prefab, transform);
                // set default values
                g.GetComponent<SpritePrefab>().SetProperties(this, vizFilesObj.files[selectedImageIndex], i, vizSettings, vizFilesObj);
                // add to prefabs list
                prefabs.Add(g);
            }
        }
    }

    /**
     *  Removes all the visualization prefabs
     *  - deletes all animations
     */
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


    public bool isPaused = false;

    void OnGUI()
    {
        if (isPaused)
            GUI.Label(new Rect(100, 100, 250, 130), "Game paused");
    }

    //void OnApplicationFocus(bool hasFocus)
    //{
    //    isPaused = !hasFocus;
    //}

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }




}


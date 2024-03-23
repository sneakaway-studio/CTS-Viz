using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SneakawayUtilities;
using UnityEngine.UI.Extensions;

/**
 *  Visualization Manager
 *  - Global references
 *  - Instantiates all sprites
 *  - Generates initial properties (they control their state afterwards)
 */

public class VizManager : MonoBehaviour
{
    public bool showDebugging = false;


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

    [Tooltip("Reference to resolution manager")]
    public ResolutionManager resolutionManager;

    [Tooltip("Collider for init position")]
    public Collider instantiateContainer;

    [Tooltip("Use a collider for init position of objects")]
    public bool useInstantiateContainerForPos = true;

    public bool isPaused = false;



    [Header("Image Collections")]

    [Tooltip("Number of images chosen")]
    public int totalImages;

    [Tooltip("Number of images to be added based on volume of WorldContainerCollider")]
    public int totalImagesWithBounds;

    public float totalImagesScale = 0.01f;

    [Tooltip("Images (and count) selected")]
    public Sprite[] selected;

    [Tooltip("The prefabs")]
    public List<GameObject> prefabs = new List<GameObject>();



    [Header("Animation Settings")]

    [Tooltip("Animate the visualization")]
    public bool animate = true;


    public Gradient2 gradient;
    public Gradient2 gradientNoise;




    [Header("Animation Direction")]

    [Tooltip("Animation direction (min)")]
    [Range(0, 1)] public float directionMin = 0.2f;
    [Tooltip("Animation direction (max)")]
    [Range(0, 1)] public float directionMax = 0.4f;

    [Tooltip("Animation rotation speed (min)")]
    [Range(0, 20)] public float directionSpeed = 1f;



    [Header("Animation Rotation")]

    [Tooltip("Animation rotation direction (min)")]
    [Range(0, 1)] public float rotateDirectionMin = 0.2f;
    [Tooltip("Animation rotation direction (max)")]
    [Range(0, 1)] public float rotateDirectionMax = 0.4f;

    [Tooltip("Animation rotation speed (min)")]
    [Range(0, 20)] public float rotateSpeed = 1f;


    private void OnValidate()
    {
        if (resolutionManager == null) resolutionManager = GetComponent<ResolutionManager>();
        // sum the total of all max values e.g. [50.50] = 100
        totalImages = vizSettings.vizFilesList.Sum(item => item.max);
        // increase images
        totalImagesWithBounds = (int)(totalImages * (resolutionManager.instantiateContainerLongestSide * totalImagesScale));
    }


    private void Awake()
    {
        if (runOnStart) Run();
    }
    // to disable in inspector
    private void Start() { }

    public void Run()
    {

        //gradient.EffectGradient = vizSettings.gradient;
        //gradientNoise.EffectGradient = vizSettings.gradient;




        // loop through the vizFile Objects
        foreach (VizFiles vizFilesObj in vizSettings.vizFilesList)
        {
            int maxWithBounds = (int)(vizFilesObj.max * (resolutionManager.instantiateContainerLongestSide * totalImagesScale));
            //Debug.Log(maxWithBounds);

            // loop for number of images to add
            for (int i = 0; i < maxWithBounds; i++)
            {
                // loop through them all once...
                int selectedImageIndex = i;
                // then after max is > than count 
                if (i >= vizFilesObj.files.Count)
                    // choose a random image for the selectedImageIndex
                    selectedImageIndex = UnityEngine.Random.Range(0, vizFilesObj.files.Count - 1);

                // alway shows all of them
                // AND we don't need to shuffle to get random
                // AND this provides safety for the loop

                // instantiate game object under this parent
                GameObject g = Instantiate(prefab, transform);
                // set default values
                g.GetComponent<SpritePrefab>().SetProperties(vizFilesObj.files[selectedImageIndex], i, vizSettings, vizFilesObj);
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
            //SpritePrefabAnim anim = child.gameObject.GetComponent<SpritePrefabAnim>();
            //if (anim.tweener != null) anim.Kill();

            Destroy(child.gameObject);
            prefabs.Clear();
        }
    }



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


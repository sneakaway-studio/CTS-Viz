using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 *  Visualization controller
 *  - Instantiates all sprites, holds|generates their properties (though they control their state afterwards)
 */

public class Visualize : MonoBehaviour
{

    [Header("Visualization Settings")]

    [Tooltip("Prefab for sprite")]
    public GameObject prefab;

    [Tooltip("Create visualization on play")]
    public bool runOnStart = true;

    [Tooltip("Delete existing visualization on play")]
    public bool clearBeforeRun = true;

    [Tooltip("Reset visualization after editing properties")]
    public bool resetPropsAfterEdit = true;




    [Header("Image Settings")]

    [Tooltip("Distance from center of Visualization position")]
    public float positionRadius = 5.0f;

    [Tooltip("Range (min/max) for scale")]
    [Range(0, 20)] public float scaleMin = 1.0f;
    [Range(0, 20)] public float scaleMax = 1.5f;

    [Tooltip("Number of images to use")]
    public int imageMin = 100;
    public int imageMax = 100;

    [Tooltip("Number of images chosen")]
    public int imageTotal;


    [Header("Image Collections")]

    [Tooltip("Images to select")]
    public Sprite[] houses;
    public Sprite[] plants;

    [Tooltip("Images (and count) selected")]
    public Sprite[] selected;

    [Tooltip("The prefabs")]
    public List<GameObject> prefabs = new List<GameObject>();

    public AnimateManager animateManager;

    private void Awake()
    {
        animateManager = GetComponent<AnimateManager>();
        if (runOnStart) Run();
    }
    private void Start()
    {
        // to disable in inspector
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            //LeanTween.cancelAll(); // 
            Destroy(child.gameObject);
            prefabs.Clear();
        }
    }

    public void Run()
    {
        if (clearBeforeRun) Clear();

        // concat the two arrays
        selected = houses.Concat(plants).ToArray();
        // then shuffle them 
        selected = Shuffle(selected);

        // the number of images to select - is set to max to allow
        // more images in the visualization
        imageTotal = (int)Mathf.Max(selected.Length, imageMax);

        for (int i = 0; i < imageTotal; i++)
        {
            // it loops through them all once
            int selectedImageIndex = i;
            // but if the max is > than the length 
            if (i >= selected.Length)
                // then choose a random image for the selectedImageIndex
                selectedImageIndex = Random.Range(0, selected.Length - 1);

            GameObject g = Instantiate(prefab,
                // these are set in child now...
                //new Vector3(
                //    Random.Range(posMin.x, posMax.x),
                //    Random.Range(posMin.y, posMax.y),
                //    i / 2),


                // these are set in child now...
                //Random.insideUnitCircle * positionRadius,
                //Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)),

                transform
            );

            // set default values
            g.GetComponent<SpritePrefab>().SetProperties(this, animateManager, selected[selectedImageIndex], i);

            prefabs.Add(g);
        }
    }


    Sprite[] Shuffle(Sprite[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++)
        {
            Sprite tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
        return arr;
    }


    // come back to this later
    //public TextAsset[] results = Array.ConvertAll(Resources.LoadAll("FolderName", typeof(TextAsset)), asset => (TextAsset)asset);


}


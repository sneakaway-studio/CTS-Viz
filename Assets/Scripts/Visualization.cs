using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Visualization : MonoBehaviour
{
    //public TextAsset[] results = Array.ConvertAll(Resources.LoadAll("FolderName", typeof(TextAsset)), asset => (TextAsset)asset);


    public bool vizOnStart = true;
    public bool clearBeforeViz = true;

    public Vector2 posMin;
    public Vector2 posMax;

    public Vector2 scaleMin;
    public Vector2 scaleMax;


    public Sprite[] houses;
    public Sprite[] plants;

    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        if (vizOnStart) Init();
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void Init()
    {
        if (clearBeforeViz) Clear();

        Sprite[] combined = houses.Concat(plants).ToArray();
        combined = Shuffle(combined);
        for (int i = 0; i < combined.Length; i++)
        {
            GameObject g = Instantiate(prefab,
                new Vector3(
                    Random.Range(posMin.x, posMax.x),
                    Random.Range(posMin.y, posMax.y),
                    i / 2),
                Quaternion.identity,
                transform
            );
            g.GetComponent<SpriteRenderer>().sprite = combined[i];
            g.GetComponent<SpriteRenderer>().sortingOrder = i;
            g.transform.localScale = new Vector3(
                Random.Range(scaleMin.x, scaleMax.x),
                Random.Range(scaleMin.y, scaleMax.y),
                1
            );
            g.transform.rotation = Random.rotation;

            if (i > combined.Length - 1) i = 0;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // look before leaping
    // https://thegamedev.guru/unity-cpu-performance/object-pooling/
    // https://docs.unity3d.com/ScriptReference/Pool.IObjectPool_1.Get.html
    // https://learn.unity.com/tutorial/introduction-to-object-pooling

    //public static ObjectPool SharedInstance;
    //public List<GameObject> pooledObjects;
    //public GameObject objectToPool;
    //public int amountToPool;

    //void Awake()
    //{
    //    SharedInstance = this;
    //}

    //void Start()
    //{
    //    CreatePool();
    //}

    //public void CreatePool()
    //{
    //    pooledObjects = new List<GameObject>();
    //    GameObject tmp;
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        tmp = Instantiate(objectToPool);
    //        tmp.SetActive(false);
    //        pooledObjects.Add(tmp);
    //    }
    //}

    //public void AddToBackToPool(List<GameObject> list)
    //{
    //    foreach(GameObject g in list)
    //    {

    //    }
    //}

    //public GameObject GetPooledObject()
    //{
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            return pooledObjects[i];
    //        }
    //    }
    //    return null;
    //}


}

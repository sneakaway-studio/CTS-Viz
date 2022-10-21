using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateManager : MonoBehaviour
{
    public Visualize visualize;


    //public SneakawayUtilities.Math.Range directionRangeX = new SneakawayUtilities.Math.Range();
    //public SneakawayUtilities.Math.Range directionRangeY = new SneakawayUtilities.Math.Range();
    //public SneakawayUtilities.Math.Range directionRangeZ = new SneakawayUtilities.Math.Range();


    [Header("Animation Settings")]

    [Tooltip("Animate the visualization")]
    public bool animate = true;


    [Tooltip("Animation movement time (higher is slower)")]
    [Range(0, 100)] public float animDirectionTime = 30f;

    [Tooltip("Range (min/max) for animation direction speed")]
    [Range(-10f, 10)] public float animDirectionMin = -0.3f;
    [Range(-10f, 10)] public float animDirectionMax = 0.3f;


    [Tooltip("Animation rotation time (higher is slower)")]
    [Range(0, 100)] public float animRotateTime = 30f;

    [Tooltip("Range (min/max) for animation rotation direction")]
    [Range(-10f, 10)] public float animRotateDirectionMin = -0.3f;
    [Range(-10f, 10)] public float animRotateDirectionMax = 0.3f;





    public void Reset()
    {



    }


    //public void Run()
    //{


    //    LeanTween.resume(directionId);

    //    //LeanTween.move(gameObject, speed, 1f).setEase(LeanTweenType.easeInQuad).setDelay(1f);
    //}
    //public void Stop()
    //{
    //    LeanTween.pause(directionId);
    //}


}

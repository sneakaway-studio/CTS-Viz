using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using DG.Tweening;

/**
 *  DOTween base class - To share time mgt props
 *  - Documentation
 *  http://dotween.demigiant.com/documentation.php?api=SetId
 *  http://dotween.demigiant.com/api/class_d_g_1_1_tweening_1_1_d_o_tween.html
 */

public class DOTweenBase : MonoBehaviour
{
    [Header("Time Mgt")]

    [Tooltip("Duration of tween (in seconds)")]
    public float duration;

    [Tooltip("Length of the tween = duration * timeScale")]
    [Range(0.001f, 5)] public float timeScale = 1;

    [HideInInspector] public float prevTimeScale = 1;

    [Tooltip("Actual timeScale (change between settings)")]
    public float computedTimeScale = 1;

    [HideInInspector] public Tweener tweener;



    private void Awake()
    {
        // initialize DOTween, increase number of tweeners allowed
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 10);
    }
    // to disable in inspector
    private void Start() { }

    /// <summary>
    /// Reset properties handler for DOTween.OnUpdate event callback
    /// </summary>
    public void OnUpdated()
    {
        // reset time scale
        if (prevTimeScale != timeScale)
        {
            prevTimeScale = timeScale;
            // transition to new time scale
            tweener.DOTimeScale(timeScale, duration);
        }
        computedTimeScale = tweener.timeScale;
    }


    // not sure if needed like it was with LeanTween
    private void OnDisable()
    {
        //LeanTween.cancel(rotationId);
    }

    public void Kill()
    {
        DOTween.Kill(tweener);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/**
 *  DOTween Documentation
 *  http://dotween.demigiant.com/documentation.php?api=SetId
 *  http://dotween.demigiant.com/api/class_d_g_1_1_tweening_1_1_d_o_tween.html
 */

public class RotateObj : MonoBehaviour
{
    [Tooltip("Start values")]
    public Vector3 start = Vector3.zero;
    [Tooltip("End values")]
    public Vector3 end = new Vector3(360, 360, 360);

    [Tooltip("The duration of the tween (in seconds)")]
    public float duration = 20;

    [Tooltip("timeScale * duration")]
    [Range(0.001f, 5)]
    public float timeScale = 1;

    float prevTimeScale = 1;

    [Tooltip("Actual timeScale tweens between settings")]
    public float computedTimeScale = 1;

    private Tweener tweener;




    private void Start()
    {
        // initialize DOTween (not sure if this is necessary)
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);

        // create Tweener
        tweener = transform
            // rotate from current to end position, over duration
            .DORotate(end, duration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear)
            .SetAutoKill(false)
            .OnUpdate(OnUpdated);

        tweener.timeScale = timeScale;
    }

    private void OnUpdated()
    {
        if (prevTimeScale != timeScale)
        {
            prevTimeScale = timeScale;
            // transition to new time scale
            tweener.DOTimeScale(timeScale, duration);
        }
        computedTimeScale = tweener.timeScale;
    }

    // not sure if needed...
    private void OnDisable()
    {
        //LeanTween.cancel(rotationId);
    }

}

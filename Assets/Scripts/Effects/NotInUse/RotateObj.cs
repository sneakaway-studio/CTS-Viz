using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using DG.Tweening;

public class RotateObj : DOTweenBase
{
    [Header("Property Mgt")]

    [Tooltip("Start values")]
    public Vector3 start = Vector3.zero;

    [Tooltip("End values")]
    public Vector3 end = new Vector3(360, 360, 360);


    private void Start()
    {
        StartTween();
    }

    private void StartTween()
    {
        // create Tweener
        tweener = transform
            // rotate from current to end position, over duration
            .DORotate(end, duration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear)
            .SetAutoKill(false)
            .OnUpdate(OnUpdated);

        tweener.timeScale = timeScale;
    }


}

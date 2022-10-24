using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObj : MonoBehaviour
{
    public int rotationId;

    public float rotateTime;
    public Vector3 rotateDirection;


    private void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);


        //transform.DOMove(new Vector3(2, 2, 2), 1);

        //DORotate(Vector3 to, float duration, RotateMode mode);
        //DORotate(float toAngle, float duration)

        transform.DORotate(new Vector3(360, 360, 360), 1, RotateMode.FastBeyond360)
            .SetLoops(3, LoopType.Restart);

    }


    private void OnDisable()
    {
        //LeanTween.cancel(rotationId);
    }

}

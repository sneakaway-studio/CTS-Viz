using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    //public bool isPaused;



    public bool runOnStart = true;

    public Vector3 rotate;
    public Vector3 direction;

    int directionId;
    int rotationId;

    private void Start()
    {
        if (runOnStart) Run();
    }

    public void Reset(Vector3 rotateRange, Vector3 directionRange)
    {
        rotate = rotateRange;
        direction = directionRange;
    }

    public void Run()
    {
        directionId = LeanTween.moveX(gameObject, 1f, 10f).setFrom(0).setRepeat(-1).id;
        rotationId = LeanTween.rotateZ(gameObject, 360, 10f).setFrom(0).setRepeat(-1).id;

        LeanTween.resume(directionId);

        //LeanTween.move(gameObject, speed, 1f).setEase(LeanTweenType.easeInQuad).setDelay(1f);
    }
    public void Stop()
    {
        LeanTween.pause(directionId);
    }


}

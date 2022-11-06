using UnityEngine;
using System.Collections;

public class CameraClass : MonoBehaviour
{

    public GameObject target;
    public float speed = 10.0f;
    private Vector3 point;

    void Start()
    {
        //get target's coords
        point = target.transform.position;
        //makes the camera look to it
        transform.LookAt(point);
    }

    void Update()
    {
        //makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier
        transform.RotateAround(point, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * speed);
    }
}
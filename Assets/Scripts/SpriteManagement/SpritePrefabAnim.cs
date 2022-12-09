using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using DG.Tweening;

public class SpritePrefabAnim : MonoBehaviour
{

    [Tooltip("To get settings in manager")]
    public VizManager vizManager;

    [Tooltip("Computed rotation vector")]
    public Vector3 randomRotateDirection;

    [Tooltip("Rotation in angles")]
    public Vector3 currentEulerAngles;

    [Tooltip("Rotation in Quaternion")]
    Quaternion currentRotation;

    [Tooltip("Limit the angles (so we don't see the 2d flat side)")]
    public MathTools.Range angleLimits = new MathTools.Range(-45, 45);

    public bool running = false;

    // assign "global" references when Editor compiles code or GO wakes
    private void OnValidate() => AssignReferences();
    private void Awake() => AssignReferences();
    void AssignReferences()
    {
        if (vizManager == null) vizManager = GameObject.Find("VizManager").GetComponent<VizManager>();
    }


    /**
     *  Set initial vars, allow animation to start
     */
    public void Init()
    {
        running = true;

        // get current rotation
        currentEulerAngles = transform.eulerAngles;

        // set start rotation values
        randomRotateDirection = MathTools.RandomVector3FromRange(
            new MathTools.Range(vizManager.rotateDirectionMin, vizManager.rotateDirectionMax)
        );
        // randomly make some negative
        if (MathTools.RandomChance(0.5f)) randomRotateDirection.x *= -1;
        if (MathTools.RandomChance(0.5f)) randomRotateDirection.y *= -1;
        if (MathTools.RandomChance(0.5f)) randomRotateDirection.z *= -1;
    }

    /**
     *  Check direction and rotate
     */
    private void FixedUpdate()
    {
        if (!running) return;

        if (transform.rotation.eulerAngles.x < angleLimits.min || transform.rotation.eulerAngles.x > angleLimits.max)
        {
            randomRotateDirection.x = randomRotateDirection.x * -1;
        }
        if (transform.rotation.eulerAngles.y < angleLimits.min || transform.rotation.eulerAngles.y > angleLimits.max)
        {
            randomRotateDirection.y = randomRotateDirection.y * -1;
        }


        // following code is from Unity Docs to prevent issues with Quaternion / Angles
        // file:///Applications/Unity/Hub/Editor/2021.3.6f1/Documentation/en/ScriptReference/Quaternion-eulerAngles.html

        // modify Vector3, multiplied by speed 
        currentEulerAngles += new Vector3(randomRotateDirection.x, randomRotateDirection.y, randomRotateDirection.z) * vizManager.rotateSpeed;

        // convert Vector3 > Quanternion.eulerAngle format
        currentRotation.eulerAngles = currentEulerAngles;

        // apply Quaternion.eulerAngles to gameObject
        transform.rotation = currentRotation;


        //transform.rotation = transform.rotation * Quaternion.Euler(rotateDirection.x, rotateDirection.y, rotateDirection.z);


    }




}

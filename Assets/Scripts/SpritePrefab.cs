using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Sprites (children) for each PNG / SVG
 *  - Get properties from base class functions
 */

public class SpritePrefab : MonoBehaviour
{
    public Visualize visualize;
    public AnimateManager animateManager;
    SpriteRenderer spriteRenderer;

    // animation IDs
    public int directionId;
    public int rotationId;

    [Header("Animation Props")]

    public float directionTime;
    public Vector3 direction;
    public float rotateTime;
    public Vector3 rotateDirection;


    public void SetProperties(Visualize _visualize, AnimateManager _animateManager, Sprite sprite, int sortingOrder)
    {
        // get parent
        visualize = _visualize;
        animateManager = _animateManager;

        // random position from radius
        transform.localPosition = Random.insideUnitCircle * visualize.positionRadius;
        // random scale from range
        transform.localScale = SneakawayUtilities.Math.RandomVector3FromRange(
            new SneakawayUtilities.Math.Range(visualize.scaleMin, visualize.scaleMax)
        );
        // random rotation
        //transform.rotation = Random.rotation;
        //transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

        //transform.rotation = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), transform.up);
        //transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        // sprite settings
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = sortingOrder;

        if (animateManager.animate)
        {
            SetAnimProperties();
            RunAnimate();
        }
    }



    public void SetAnimProperties()
    {
        direction = SneakawayUtilities.Math.RandomVector3FromRange(
            new SneakawayUtilities.Math.Range(animateManager.animDirectionMin, animateManager.animDirectionMax)
        );
        rotateDirection = SneakawayUtilities.Math.RandomVector3FromRange(
            new SneakawayUtilities.Math.Range(animateManager.animRotateDirectionMin, animateManager.animRotateDirectionMax)
        );

    }
    public void RunAnimate()
    {
        SetAnimProperties();

        directionId = LeanTween.move(gameObject, direction, animateManager.animDirectionTime).setRepeat(-1).id;
        rotationId = LeanTween.rotate(gameObject, rotateDirection, animateManager.animRotateTime).setRepeat(-1).id;
    }

    private void OnDisable()
    {
        LeanTween.cancel(directionId);
        LeanTween.cancel(rotationId);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;

/**
 *  Sprites (children) for each PNG / SVG
 *  - Get properties from base class functions
 */

public class SpritePrefab : MonoBehaviour
{
    public Visualize visualize;
    SpriteRenderer spriteRenderer;
    SpritePrefabAnim spritePrefabAnim;
    VizSettings vizSettings;
    public VizFiles vizFilesObj;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void SetProperties(Visualize _visualize, Sprite sprite,
        int sortingOrder, VizSettings _vizSettings, VizFiles _vizFilesObj)
    {
        vizSettings = _vizSettings;
        vizFilesObj = _vizFilesObj;

        // get parent
        visualize = _visualize;
        spritePrefabAnim = GetComponent<SpritePrefabAnim>();


        // random position from radius
        transform.localPosition = Random.insideUnitCircle * vizFilesObj.positionRadius;
        // random scale from range
        transform.localScale = Math.RandomVector3FromRange(
            new Math.Range(vizFilesObj.scaleMin, vizFilesObj.scaleMax)
        );
        // random rotation
        transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        // sprite settings        
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = sortingOrder;

        if (visualize.animate)
        {
            spritePrefabAnim.UpdateTween(vizSettings);
        }
    }




}

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
    public Visualization visualization;
    SpriteRenderer spriteRenderer;
    SpritePrefabAnim spritePrefabAnim;
    VizSettings vizSettings;
    public VizFiles vizFilesObj;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // to disable in inspector
    private void Start() { }


    public void SetProperties(Visualization _visualization, Sprite _sprite,
        int _sortingOrder, VizSettings _vizSettings, VizFiles _vizFilesObj)
    {
        vizSettings = _vizSettings;
        vizFilesObj = _vizFilesObj;






        // get parent
        visualization = _visualization;
        spritePrefabAnim = GetComponent<SpritePrefabAnim>();

        // change name
        //gameObject.name = 

        //////////////////////////////////////////////
        ////// RANDOMIZE TRANSFORM PROPERTIES ////////
        //////////////////////////////////////////////

        // random position from radius
        transform.localPosition = Random.insideUnitCircle * vizFilesObj.positionRadius;

        // random scale from range
        float r = Math.RandomFloatFromRange(new Math.Range(vizFilesObj.scaleMin, vizFilesObj.scaleMax));
        transform.localScale = new Vector3(r, r, 1);

        // random rotation
        transform.rotation = Math.RandomQuaternion();

        // sprite settings        
        spriteRenderer.sprite = _sprite;
        spriteRenderer.sortingOrder = _sortingOrder;

        //if (visualization.animate)
        //{
        spritePrefabAnim.UpdateTween(vizSettings);
        //}
    }




}

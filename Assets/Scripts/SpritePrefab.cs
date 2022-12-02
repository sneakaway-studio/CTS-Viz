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
        visualization = _visualization;
        vizSettings = _vizSettings;
        vizFilesObj = _vizFilesObj;
        spritePrefabAnim = GetComponent<SpritePrefabAnim>();



        //////////////////////////////////////////////
        ////// RANDOMIZE TRANSFORM PROPERTIES ////////
        //////////////////////////////////////////////

        // random position
        if (visualization.useWorldColliderForPos)
        {
            // random inside collider bounds
            transform.localPosition = MathTools.RandomPointInBounds(visualization.worldContainerCollider.bounds);
        }
        else
        {
            // random from radius
            transform.localPosition = Random.insideUnitCircle * vizFilesObj.positionRadius;
        }

        // random scale from range
        float r = MathTools.RandomFloatFromRange(new MathTools.Range(vizFilesObj.scaleMin, vizFilesObj.scaleMax));
        transform.localScale = new Vector3(r, r, 1);

        // random rotation
        transform.rotation = MathTools.RandomQuaternion();

        // sprite settings        
        spriteRenderer.sprite = _sprite;
        spriteRenderer.sortingOrder = _sortingOrder;
        // change name
        gameObject.name = _sprite.name;

        // start animation
        if (visualization.animate)
        {
            spritePrefabAnim.UpdateTween(vizSettings);
        }
    }




}

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
    public VizManager vizManager;
    SpriteRenderer spriteRenderer;
    SpritePrefabAnim spritePrefabAnim;
    public VizSettings vizSettings;
    public VizFiles vizFilesObj;

    // assign "global" references when Editor compiles code or GO wakes
    private void OnValidate() => AssignReferences();
    private void Awake() => AssignReferences();
    void AssignReferences()
    {
        if (vizManager == null) vizManager = GameObject.Find("VizManager").GetComponent<VizManager>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }



    // to disable in inspector
    private void Start() { }


    public void SetProperties(Sprite _sprite, int _sortingOrder, VizSettings _vizSettings, VizFiles _vizFilesObj)
    {
        vizSettings = _vizSettings;
        vizFilesObj = _vizFilesObj;
        spritePrefabAnim = GetComponent<SpritePrefabAnim>();



        //////////////////////////////////////////////
        ////// RANDOMIZE TRANSFORM PROPERTIES ////////
        //////////////////////////////////////////////

        // random position
        if (vizManager.useWorldColliderForPos)
        {
            // random inside collider bounds
            transform.localPosition = MathTools.RandomPointInBounds(vizManager.worldContainerCollider.bounds);
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
        if (vizManager.animate)
        {
            spritePrefabAnim.UpdateTween(vizSettings);
        }
    }




}

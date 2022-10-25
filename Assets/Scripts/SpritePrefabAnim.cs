using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SneakawayUtilities;
using DG.Tweening;

public class SpritePrefabAnim : DOTweenBase
{
    // _DOTweenBase_PROPS_HERE_


    [Header("Property Mgt")]

    [Tooltip("Computed direction vector")]
    public Vector3 direction;
    [Tooltip("Computed rotation vector")]
    public Vector3 rotateDirection;
    [Tooltip("Reference to vizSettings")]
    public VizSettings vizSettingsObj;


    /**
     *  Update the tween
     */
    public void UpdateTween(VizSettings _vizSettingsObj)
    {
        // store reference
        vizSettingsObj = _vizSettingsObj;

        // set new values
        direction = Math.RandomVector3FromRange(
            new Math.Range(vizSettingsObj.animDirectionMin, vizSettingsObj.animDirectionMax)
        );
        rotateDirection = Math.RandomVector3FromRange(
            new Math.Range(vizSettingsObj.animRotateDirectionMin, vizSettingsObj.animRotateDirectionMax)
        );


        // create Tweener
        tweener = transform
            // rotate from current to end position, over duration
            .DORotate(rotateDirection, vizSettingsObj.animRotateTime, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart) // loop
            .SetEase(Ease.Linear) // no easing
            .SetAutoKill(true) // kill on destroy??
            .OnUpdate(OnUpdated); // on each update call func in parent

        // update the timeScale
        tweener.timeScale = timeScale;
    }




}

using UnityEngine;
using System.Collections;

namespace SneakawayUtilities
{
    public static class PhysicsHelpers
    {




        public static bool CheckCollision(Collision2D collision, string nameToCompare)
        {
            return collision.gameObject.layer == LayerMask.NameToLayer(nameToCompare);
        }

        public static void SetGameObjectLayerMask(GameObject obj, string layerMaskName)
        {
            // convert layermask name to layer integer 
            int LayerIgnoreRaycast = LayerMask.NameToLayer(layerMaskName);
            // assign to layer
            obj.layer = LayerIgnoreRaycast;
            //Debug.Log("Current layer: " + obj.layer);
        }



    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SneakawayUtilities
{
    public static class SpriteExtensions
    {



        public static Sprite[] Shuffle(Sprite[] arr)
        {
            // Knuth shuffle algorithm :: courtesy of Wikipedia :)
            for (int t = 0; t < arr.Length; t++)
            {
                Sprite tmp = arr[t];
                int r = UnityEngine.Random.Range(t, arr.Length);
                arr[t] = arr[r];
                arr[r] = tmp;
            }
            return arr;
        }



    }
}
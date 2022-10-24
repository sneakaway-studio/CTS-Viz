using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SneakawayUtilities
{
    public static class Math
    {

        /////////////////////////////
        /////////// TYPES ///////////
        /////////////////////////////

        /// <summary>
        /// Range struct with min/max
        /// </summary>
        [System.Serializable]
        public struct Range
        {
            public float min;
            public float max;
            public Range(float _min, float _max)
            {
                min = _min;
                max = _max;
            }
        }



        /////////////////////////////
        //////// EXTENSIONS /////////
        /////////////////////////////

        /// <summary>Return a random int</summary>
        /// <param name="min">int</param>
        /// <param name="max">int</param>
        /// <returns>int</returns>
        public static int RandomInt(this int min, int max)
        {
            // they are both zero
            if (min == 0 && max == 0) return 0;
            // they are the same, just return one
            if (min == max) return min;
            // else generate random and return
            int r = (int)UnityEngine.Random.Range(min, max);
            //Debug.Log(r);
            return r;
        }
        /// <summary>Return a random int (from floats)</summary>
        /// <param name="min">float</param>
        /// <param name="max">float</param>
        /// <returns>int</returns>
        public static int RandomInt(this float min, float max)
        {
            // they are both zero
            if (min == 0 && max == 0) return 0;
            // they are the same, just return one
            if (min == max) return (int)min;
            // else generate random and return
            int r = (int)UnityEngine.Random.Range(min, max);
            //Debug.Log(r);
            return r;
        }


        /// <summary>Return a random float within a Range</summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static float RandomFloatFromRange(this Range range)
        {
            // https://docs.unity3d.com/ScriptReference/Random.Range.html
            return UnityEngine.Random.Range(range.min, range.max);
        }


        public static float RandomFloatFromVector2(this Vector2 range)
        {
            if (range.magnitude < 0.01)
                return 0;
            float r = Random.Range(range.x, range.y);
            //Debug.Log(r);
            return r;
        }


        /// <summary>
        /// Return a Vector3 with random X,Y,Z from Range
        /// </summary>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        /// <param name="rangeZ"></param>
        /// <returns></returns>
        public static Vector3 RandomVector3FromRange(
            this Range rangeX, Range rangeY, Range rangeZ)
        {
            return new Vector3(
                Random.Range(rangeX.min, rangeX.max),
                Random.Range(rangeY.min, rangeY.max),
                Random.Range(rangeZ.min, rangeZ.max)
            );
        }
        public static Vector3 RandomVector3FromRange(
            this Range range)
        {
            return new Vector3(
                Random.Range(range.min, range.max),
                Random.Range(range.min, range.max),
                Random.Range(range.min, range.max)
            );
        }







        /// <summary>
        /// Return a random index from any List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int RandomListIndex<T>(this List<T> list)
        {
            return (int)Random.Range(0, list.Count);
        }







    }
}

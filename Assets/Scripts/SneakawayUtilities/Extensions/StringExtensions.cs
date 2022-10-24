using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SneakawayUtilities
{
    public static class StringExtensions
    {

        /// <summary>
        /// Log the length of a string
        /// </summary>
        /// <param name="str"></param>
        public static void LogLength(this string str)
        {
            Debug.Log($"'{str}' (string) contains {str.Length} characters");
        }


    }
}
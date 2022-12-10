using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class AssetManager : MonoBehaviour
{
    [Tooltip("ScriptableObject with settings")]
    public VizSettings vizSettings;

    // folder path in Assets/
    public static string assetPath = "Resources/UTC-ORIGINALS-PNG/";



#if UNITY_EDITOR


    // sprite folder in assetPath/
    public string spriteFolder = "00-00/01/house/PNG/";


    public Sprite[] sprites;






    /**
     *  Get all assets at path
     */
    public Sprite[] GetSpritesAtPath(string spriteFolder)
    {
        // default to none
        var _sprites = new Sprite[0];

        // check that path exists (game data folder on target device + assetPath + spriteFolder)
        if (!System.IO.Directory.Exists($"{Application.dataPath}/{assetPath}/{spriteFolder}"))
        {
            Debug.LogWarning($"Path does not exist: {assetPath}/{spriteFolder}");
            return _sprites;
        }

        // path to target folder
        var folderPath = new string[] { $"Assets/{assetPath}/{spriteFolder}" };
        // search for asset using type (t) or label (l)
        var guids = AssetDatabase.FindAssets("t:Sprite", folderPath);

        // create new array using found length
        var newSprites = new Sprite[guids.Length];


        bool fileListUpdated = false;
        if (_sprites != newSprites)
        {
            fileListUpdated = true;
            _sprites = newSprites;
        }
        else
        {
            fileListUpdated = newSprites.Length != sprites.Length;
        }

        // add sprites to array
        for (int i = 0; i < newSprites.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            newSprites[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            fileListUpdated |= (i < sprites.Length && sprites[i] != newSprites[i]);
        }

        if (fileListUpdated)
        {
            _sprites = newSprites;
            Debug.Log($"{name} sprite list updated.");
        }

        return _sprites;
    }
#endif



}




using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/**
 *  Script to encapsulate asset collecting 
 */

public class AssetManagerStatic
{
    public static Sprite[] sprites = new Sprite[0];

    /**
     *  Get all assets at path
     *   - This method only works in the editor https://stackoverflow.com/a/55479867/441878
     */
#if UNITY_EDITOR
    public static Sprite[] GetSpritesAtPathEditorOnly(string assetPath, string spriteFolder)
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
        var guids = UnityEditor.AssetDatabase.FindAssets("t:Sprite", folderPath);

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
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
            newSprites[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
            fileListUpdated |= (i < sprites.Length && sprites[i] != newSprites[i]);
        }

        if (fileListUpdated)
        {
            _sprites = newSprites;
            Debug.Log($"sprite list updated.");
        }

        return _sprites;
    }
#endif

    public static Sprite[] GetSpritesFromResources(string assetPath, string spriteFolder)
    {
        var appRelativeFolderPath = $"Resources/{assetPath}/{spriteFolder}";
        Debug.Log($"### 0 ### AssetManagerStatic.GetSpritesAtPath() appRelativeFolderPath={appRelativeFolderPath}");

        // default to none
        var _sprites = new Sprite[0];

#if UNITY_EDITOR
        // EDITOR ONLY (JUST FOR TESTING)
        // check that path exists (game data folder on target device + assetPath + spriteFolder)
        if (!System.IO.Directory.Exists($"{Application.dataPath}/{appRelativeFolderPath}"))
        {
            Debug.LogWarning($"### X ### AssetManagerStatic.GetSpritesAtPath() Path does not exist: {Application.dataPath}/{appRelativeFolderPath}");
            return _sprites;
        }
#endif

        var folderPath = $"{assetPath}/{spriteFolder}";
        Debug.Log($"### 1 ### AssetManagerStatic.GetSpritesAtPath() folderPath={appRelativeFolderPath}");

        var newSprites = Resources.LoadAll<Sprite>(folderPath);
        Debug.Log($"### 2 ### AssetManagerStatic.GetSpritesAtPath() newSprites.Length={newSprites.Length}");


        if (_sprites != newSprites)
        {
            _sprites = newSprites;
            Debug.Log($"sprite list updated.");
        }

        return _sprites;
    }

}


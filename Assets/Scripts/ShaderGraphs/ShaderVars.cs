using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 *  Base class to allow changing specific shader variables on material
 *  - Changing vars in shader settings affects all instance (it defaults to renderer.sharedMaterial)
 *  - So, this class allows instance vars
 */

public abstract class ShaderVars : MonoBehaviour
{
    public enum RendererType
    {
        SpriteRenderer,
        MeshRenderer,
        Image,
        RawImage
    }
    public RendererType rendererType;
    [HideInInspector] public Material material;

    public void Awake()
    {
        // allows use with any type of renderer
        switch (rendererType)
        {
            case RendererType.SpriteRenderer:
                material = GetComponent<SpriteRenderer>().material;
                break;
            case RendererType.MeshRenderer:
                material = GetComponent<MeshRenderer>().material;
                break;
            case RendererType.Image:
                material = GetComponent<Image>().material;
                break;
            case RendererType.RawImage:
                material = GetComponent<RawImage>().material;
                break;
        }
    }

    // override in child class
    public abstract void UpdateShader();




}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 *  Base class to allow changing specific shader variables on material
 *  - if you change it on the shader settings then it changes them all
 *  - ^ it defaults to renderer.sharedMaterial
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

    public abstract void UpdateShader();




}

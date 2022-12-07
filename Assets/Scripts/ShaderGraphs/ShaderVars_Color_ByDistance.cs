using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Change color of object based on distance from point
 */

public class ShaderVars_Color_ByDistance : ShaderVars
{

    public enum ShaderAxis
    {
        X, Y, Z, A
    }
    public ShaderAxis shaderAxis;
    public Color colorA;
    public Color colorB;


    private void Update()
    {
        UpdateShader();
    }

    public override void UpdateShader()
    {
        material.SetColor("_ColorA", colorA);
        material.SetColor("_ColorB", colorB);

        // set the enum
        material.DisableKeyword("_AXIS_X");
        material.DisableKeyword("_AXIS_Y");
        material.DisableKeyword("_AXIS_Z");
        material.DisableKeyword("_AXIS_A");

        switch (shaderAxis)
        {
            case ShaderAxis.X:
                {
                    material.EnableKeyword("_AXIS_X");
                    break;
                }
            case ShaderAxis.Y:
                {
                    material.EnableKeyword("_AXIS_Y");
                    break;
                }
            case ShaderAxis.Z:
                {
                    material.EnableKeyword("_AXIS_Z");
                    break;
                }
            case ShaderAxis.A:
                {
                    material.EnableKeyword("_AXIS_A");
                    break;
                }
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellColourModifier : MonoBehaviour
{
    Light mainLight;
    Color lastColour;

    void Start()
    {
        mainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
    }

    void FixedUpdate()
    {
        if (lastColour != mainLight.color)
        {
            MeshRenderer[] renderers = GameObject.FindObjectsOfType<MeshRenderer>();

            foreach (MeshRenderer i in renderers)
            {
                Material temp = i.material;
            
                if (temp.shader.name.Equals("FlexibleCelShader/Cel Outline"))
                {
                    //temp.color = mainLight.color;

                    float shaderHue, shaderSaturation, shaderValue;
                    float lightHue, lightSaturation, lightValue;
                
                    Color.RGBToHSV(temp.color, out shaderHue, out shaderSaturation, out shaderValue);
                    Color.RGBToHSV(mainLight.color, out lightHue, out lightSaturation, out lightValue);

                    temp.color = Color.HSVToRGB(
                        FloatInterpolate(shaderHue, lightHue),
                        FloatInterpolate(shaderSaturation, lightSaturation),
                        FloatInterpolate(shaderValue, lightValue / 100)
                    );
                }
            }

            lastColour = mainLight.color;
        }
    }

    float FloatInterpolate(float f1, float f2)
    {
        float temp = f1;

        if (f2 > f1)
        {
            temp += f2;
        }
        else if (f2 < f1)
        {
            temp -= f2;
        }

        return temp;
    }
}

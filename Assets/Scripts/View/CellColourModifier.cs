using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellColourModifier : MonoBehaviour
{
    Light mainLight;
    Color lastColour;
    Dictionary<MeshRenderer, Color> dict;

    void Start()
    {
        mainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
        dict = new Dictionary<MeshRenderer, Color>();
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
                    if (!dict.Keys.Contains(i))
                    {
                        dict.Add(i, temp.color);
                    }
                    
                    float shaderHue, shaderSaturation, shaderValue;
                    float lightHue, lightSaturation, lightValue;
                
                    Color.RGBToHSV(dict[i], out shaderHue, out shaderSaturation, out shaderValue);
                    Color.RGBToHSV(mainLight.color, out lightHue, out lightSaturation, out lightValue);

                    float tempHue = shaderHue;
                    float tempSaturation = shaderSaturation;
                    float tempValue = (shaderValue * lightValue) * 0.6f + 0.1f;
                    
                    if (temp.mainTexture != null)
                    {
                        tempHue += lightHue;
                        tempSaturation += lightSaturation / 3f;
                        tempValue += 0.2f;
                    }

                    temp.color = Color.HSVToRGB(tempHue, tempSaturation, tempValue);
                }
            }

            lastColour = mainLight.color;
        }
    }
}

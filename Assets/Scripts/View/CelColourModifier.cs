using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CelColourModifier : MonoBehaviour
{
    Light mainLight; // Main scene light
    Color lastColour; // The last saved colour of the mainLight
    Dictionary<MeshRenderer, Color> mRendererDict; // Dictionary of saved initial MeshRenderer shader colours
    Dictionary<SpriteRenderer, Color> sRendererDict; // Dictionary of saved initial SpriteRenderer colours

    void Start()
    {
        mainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
        
        mRendererDict = new Dictionary<MeshRenderer, Color>();
        sRendererDict = new Dictionary<SpriteRenderer, Color>();
    }

    void FixedUpdate()
    {
        // Only recalculating when the light changes
        if (lastColour != mainLight.color)
        {
            MeshRenderer[] mRenderers = FindObjectsOfType<MeshRenderer>(); // Getting all of the MeshRenderers in the scene

            foreach (MeshRenderer i in mRenderers)
            {
                Material temp = i.material;
                
                // Making sure that the material's shader is one of the cel shaders
                if (temp.shader.name.Contains("Cel"))
                {
                    // Adding new initial colours (if necessary)
                    if (!mRendererDict.Keys.Contains(i))
                    {
                        mRendererDict.Add(i, temp.color);
                    }

                    temp.color = ApplyLight(mRendererDict[i]);
                }
            }
            
            SpriteRenderer[] sRenderers = FindObjectsOfType<SpriteRenderer>(); // Getting all of the SpriteRenderers in the scene

            foreach (SpriteRenderer j in sRenderers)
            {
                // Adding new initial colours (if necessary)
                if (!sRendererDict.Keys.Contains(j))
                {
                    sRendererDict.Add(j, j.color);
                }

                j.color = ApplyLight(sRendererDict[j]);
            }

            lastColour = mainLight.color; // Resetting the last colour
        }
    }

    /// <summary>
    /// Applies the current main light's colour to a given colour
    /// </summary>
    /// <param name="col">Colour to be modified</param>
    /// <returns>A new colour, modified from the light colour</returns>
    Color ApplyLight(Color col)
    {
        float shaderHue, shaderSaturation, shaderValue;
        Color.RGBToHSV(col, out shaderHue, out shaderSaturation, out shaderValue); // Initial shader HSV colour
                    
        float lightHue, lightSaturation, lightValue;
        Color.RGBToHSV(mainLight.color, out lightHue, out lightSaturation, out lightValue); // Light HSV colour

        // Setting the default HSV amounts
        float tempHue = shaderHue + lightHue;
        float tempSaturation = shaderSaturation + (lightSaturation / 1.5f);
        float tempValue = (shaderValue * lightValue) * 0.6f + 0.4f; // Applying the light's value to the shader (with a minimum)

        return Color.HSVToRGB(tempHue, tempSaturation, tempValue); // setting the new colour to the material
    }
}
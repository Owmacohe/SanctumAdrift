using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellColourModifier : MonoBehaviour
{
    Light mainLight; // Main scene light
    Color lastColour; // The last saved colour of the mainLight
    Dictionary<MeshRenderer, Color> dict; // Dictionary of saved initial shader colours

    void Start()
    {
        mainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
        dict = new Dictionary<MeshRenderer, Color>();
    }

    void FixedUpdate()
    {
        // Only recalculating when the light changes
        if (lastColour != mainLight.color)
        {
            MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>(); // Getting all of the MeshRenderers in the scene

            foreach (MeshRenderer i in renderers)
            {
                Material temp = i.material;
                
                // Making sure that the material's shader is the cel shader
                if (temp.shader.name.Equals("FlexibleCelShader/Cel Outline"))
                {
                    // Adding new initial colours (if necessary)
                    if (!dict.Keys.Contains(i))
                    {
                        dict.Add(i, temp.color);
                    }
                    
                    float shaderHue, shaderSaturation, shaderValue;
                    float lightHue, lightSaturation, lightValue;
                
                    Color.RGBToHSV(dict[i], out shaderHue, out shaderSaturation, out shaderValue); // Initial shader HSV colour
                    Color.RGBToHSV(mainLight.color, out lightHue, out lightSaturation, out lightValue); // Light HSV colour

                    float tempHue = shaderHue;
                    float tempSaturation = shaderSaturation;
                    float tempValue = (shaderValue * lightValue) * 0.6f + 0.1f; // Applying the light's value to the shader (with a minimum)
                    
                    // If the shader has an image texture...
                    if (temp.mainTexture != null)
                    {
                        tempHue += lightHue; // The light's hue gets added
                        tempSaturation += lightSaturation / 3f; // Some of the light's saturation gets added
                        tempValue += 0.2f; // The value is boosted
                    }

                    temp.color = Color.HSVToRGB(tempHue, tempSaturation, tempValue); // setting the new colour to the material
                }
            }

            lastColour = mainLight.color; // Resetting the last colour
        }
    }
}
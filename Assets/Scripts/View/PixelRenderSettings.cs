using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelRenderSettings : MonoBehaviour
{
    public enum PixelationTypes { None, Medium, Large, Huge, Enormous };
    [Tooltip("Amount of pixelation to be applied to the camera resolution")]
    public PixelationTypes pixelation = PixelationTypes.Large;

    private void Start()
    {
        int width = 1920;
        int height = 1080;

        try
        {
            switch (pixelation)
            {
                case PixelationTypes.None:
                    width = 1920;
                    height = 1080;
                    break;
                case PixelationTypes.Medium:
                    width = 480;
                    height = 270;
                    break;
                case PixelationTypes.Large:
                    width = 320;
                    height = 180;
                    break;
                case PixelationTypes.Huge:
                    width = 160;
                    height = 90;
                    break;
                case PixelationTypes.Enormous:
                    width = 80;
                    height = 45;
                    break;
            }
        }
        catch { }

        // Setting the resolution
        RawImage image = GetComponent<RawImage>();
        image.texture.width = width;
        image.texture.height = height;
    }
}

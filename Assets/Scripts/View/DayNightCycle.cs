using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("Speed at which the light rotates")]
    [SerializeField] float speed = 3;
    [Tooltip("Colour that the light transitions to at night")]
    [SerializeField] Color nightColour;

    [HideInInspector] public bool isNight; // Whether it is currently night time
    Color dayColour; // Default light colour
    Light mainLight; // Main light source in the scene

    void Start()
    {
        mainLight = GameObject.FindWithTag("MainLight").GetComponent<Light>();
        dayColour = mainLight.color;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, speed * 0.0025f, Space.World);

        float temp = transform.eulerAngles.y;
        
        // Lerping towards the night colour when between 0 and 180
        if (temp >= 0 && temp <= 180)
        {
            mainLight.color = Color.Lerp(dayColour, nightColour, temp / 180f);
        }
        // Lerping towards the day colour when between 180 and 360
        else
        {
            mainLight.color = Color.Lerp(nightColour, dayColour, (temp - 180f) / 180f);
        }

        // Night time occurs between 90 and 270
        if (temp >= 90 && temp <= 270) {
            isNight = true;
        }
        else
        {
            isNight = false;
        }
    }
}

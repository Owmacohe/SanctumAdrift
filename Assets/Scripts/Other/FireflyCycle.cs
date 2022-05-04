using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyCycle : MonoBehaviour
{
    DayNightCycle cycle; // Day/night cycle upon which the fireflies are dependant
    ParticleSystem fireflies; // This GameObject's particle system

    void Start()
    {
        cycle = FindObjectOfType<DayNightCycle>();
        fireflies = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        // Ignores the cycle if it doesn't exist
        if (cycle != null)
        {
            // Fireflies appear at night and go away during the day
            if (cycle.isNight && !fireflies.isPlaying)
            {
                fireflies.Play();
            }
            else if (!cycle.isNight && fireflies.isPlaying)
            {
                fireflies.Stop();
            }
        }
    }
}

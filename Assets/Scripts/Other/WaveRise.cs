using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRise : MonoBehaviour
{
    [Tooltip("Highest that the water can rise above the starting level")]
    [Range(0.1f, 1)] [SerializeField] float maxRise = 0.5f;
    [Tooltip("Chance that a wave can happen each frame")]
    [Range(1, 20)] [SerializeField] float waveFrequency = 15;
    [Tooltip("Speed that the wave rises at")]
    [Range(0.1f, 2)] [SerializeField] float waveSpeed = 0.8f;

    float startRise;
    bool isWaving, hasHitTop;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        startRise = transform.localPosition.y;
    }

    void FixedUpdate()
    {
        // Making sure that a new wave only starts if no others are currently happening
        if (!isWaving && Random.Range(0, 100) <= waveFrequency)
        {
            isWaving = true;
        }

        if (isWaving)
        {
            if (!hasHitTop)
            {
                if (transform.localPosition.y < startRise + maxRise)
                {
                    // Pushing the wave up if it hasn't hit its peak
                    rb.velocity = Vector3.up * waveSpeed;
                }
                else
                {
                    hasHitTop = true;
                }
            }
            // Pushing the wave down (slowly) if it has hit its peak
            else
            {
                rb.velocity = Vector3.up * -(waveSpeed / 1000);
            }
        }

        // Resetting the water height if its less than its starting height
        if (transform.localPosition.y < startRise)
        {
            transform.localPosition = Vector3.up * startRise;

            isWaving = false;
            hasHitTop = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRise : MonoBehaviour
{
    [Range(0.1f, 1)]
    public float maxRise = 0.5f;
    [Range(1, 20)]
    public float waveFrequency = 15;
    [Range(0.1f, 2)]
    public float waveSpeed = 0.8f;

    private float startRise;
    private bool isWaving, hasHitTop;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        startRise = transform.localPosition.y;
    }

    private void FixedUpdate()
    {
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
                    rb.velocity = Vector3.up * waveSpeed;
                }
                else
                {
                    hasHitTop = true;
                }
            }
            else
            {
                rb.velocity = Vector3.up * -(waveSpeed / 1000);
            }
        }

        if (transform.localPosition.y < startRise)
        {
            transform.localPosition = Vector3.up * startRise;

            isWaving = false;
            hasHitTop = false;
        }
    }
}

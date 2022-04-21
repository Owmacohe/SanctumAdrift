using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleGoOut : MonoBehaviour
{
    bool hasGoneOut;

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= 200)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasGoneOut && collision.relativeVelocity.magnitude >= 2)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            hasGoneOut = true;
        }
    }
}
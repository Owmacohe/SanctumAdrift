using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleGoOut : MonoBehaviour
{
    [Tooltip("The smoke particle system object to be created when the candle goes out")]
    [SerializeField] GameObject smokePuff;

    bool hasGoneOut; // Whether the candle has been disturbed and the fire has gone out

    void FixedUpdate()
    {
        // Deleting the candle if it falls out of the world
        if (Vector3.Distance(transform.position, Vector3.zero) >= 200)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Disabling the fire particles if the candle gets bumped too hard
        if (!hasGoneOut && collision.relativeVelocity.magnitude >= 2)
        {
            GameObject temp = Instantiate(smokePuff, transform);
            temp.transform.position = transform.GetChild(0).position;

            transform.GetChild(0).gameObject.SetActive(false);
            hasGoneOut = true;
        }
    }
}
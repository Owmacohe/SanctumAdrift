using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomTilt : MonoBehaviour
{
    [Tooltip("The maximum amount in degrees that the object can be tilted in either direction")]
    [SerializeField] float maxTilt = 30;

    void Start()
    {
        transform.Rotate(transform.forward, Random.Range(-maxTilt, maxTilt));
    }
}

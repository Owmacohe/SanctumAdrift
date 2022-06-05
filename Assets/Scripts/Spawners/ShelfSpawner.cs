using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShelfSpawner : MonoBehaviour
{
    [Tooltip("The shelf prefabs to be spawned")]
    [SerializeField] GameObject[] shelves;

    void Start()
    {
        Instantiate(shelves[Random.Range(0, shelves.Length)], transform);
    }
}
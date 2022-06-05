using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChanceToSpawn : MonoBehaviour
{
    [Tooltip("The chance out of 10 that the object will actually appear")]
    [SerializeField] [Range(0, 10)] int spawnChance = 5;
    
    void Start()
    {
        if (Random.Range(0, 11) > spawnChance)
        {
            gameObject.SetActive(false);
        }
    }
}
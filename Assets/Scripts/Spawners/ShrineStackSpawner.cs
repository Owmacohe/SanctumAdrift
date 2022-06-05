using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineStackSpawner : MonoBehaviour
{
    [Tooltip("Number of shrines to be included in the stack")]
    [Range(1, 3)] [SerializeField] int stackNum = 1;
    [Tooltip("Whether the stackNum is random")]
    [SerializeField] bool isRandomStackNum = true;
    [Tooltip("Whether the rotation of each shrine is random")]
    [SerializeField] bool isRotationRandom = true;
    [Tooltip("List of 3 possible shrine stacks to be spawned")]
    [SerializeField] GameObject[] stacks;
    
    void Start()
    {
        // Checking if the stackNum is random
        if (isRandomStackNum)
        {
            stackNum = Random.Range(1, stacks.Length + 1);
        }

        GameObject temp = Instantiate(stacks[stackNum - 1], transform);

        // Checking if the shrine stack rotation is random
        if (isRotationRandom)
        {
            temp.transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
        }
    }
}

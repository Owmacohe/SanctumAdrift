using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineStackSpawner : MonoBehaviour
{
    [Range(1, 3)] [SerializeField] int stackNum = 1;
    [SerializeField] bool isRandomStackNum = true;
    [SerializeField] bool isRotationRandom = true;

    [SerializeField] GameObject[] stacks;
    
    void Start()
    {
        if (isRandomStackNum)
        {
            stackNum = Random.Range(1, stacks.Length + 1);
        }

        GameObject temp = Instantiate(stacks[stackNum - 1], transform);

        if (isRotationRandom)
        {
            temp.transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
        }
    }
}

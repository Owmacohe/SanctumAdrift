using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PrefabSpawner : MonoBehaviour
{
    [Tooltip("Number of prefabs to be included in the cluster")]
    [Range(1, 3)] [SerializeField] int multiNum = 1;
    [Tooltip("Whether the multiNum is random")]
    [SerializeField] bool isRandomMultiNum = true;
    [Tooltip("Whether the rotation of each prefab is random")]
    [SerializeField] bool isRotationRandom = true;
    [Tooltip("List of 3 possible prefabs to be spawned in the cluster")]
    [SerializeField] GameObject[] prefabList;

    readonly Vector2[] multi2Positions = { Vector2.one * 0.3f, -Vector2.one * 0.3f }; // Local positions if the multiNum is 2
    readonly Vector2[] multi3Positions = { new Vector2(0, -0.5f), new Vector2(0.5f, 0.5f), new Vector2(-0.5f, 0.5f) }; // Local positions if the multiNum is 3

    void Start()
    {
        // Checking if the multiNum is random
        if (isRandomMultiNum)
        {
            multiNum = Random.Range(1, 4);
        }

        for (int i = 0; i < multiNum; i++)
        {
            // Creating a new GameObject from a random prefab in the prefabList
            int tempIndex = Random.Range(0, prefabList.Length);
            GameObject temp = Instantiate(prefabList[tempIndex], transform);

            // Checking if the prefab rotation is random
            if (isRotationRandom)
            {
                temp.transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
            }
            
            // Setting the positions if the multiNum is > 1
            if (multiNum == 2)
            {
                temp.transform.localPosition = new Vector3(multi2Positions[i].x, 0, multi2Positions[i].y);
            }
            else if (multiNum == 3)
            {
                temp.transform.localPosition = new Vector3(multi3Positions[i].x, 0, multi3Positions[i].y);
            }

            temp.transform.Translate(Vector3.up * 0.03f, Space.Self); // Pushing the prefabs up a bit
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PrefabSpawner : MonoBehaviour
{
    [Range(1, 3)] [SerializeField] int multiNum = 1;
    [SerializeField] bool isRandomMultiNum = true;
    [SerializeField] bool isRotationRandom = true;
    [SerializeField] GameObject[] prefabList;

    readonly Vector2[] multi2Positions = { Vector2.one * 0.3f, -Vector2.one * 0.3f };
    readonly Vector2[] multi3Positions = { new Vector2(0, -0.5f), new Vector2(0.5f, 0.5f), new Vector2(-0.5f, 0.5f) };

    void Start()
    {
        if (isRandomMultiNum)
        {
            multiNum = Random.Range(1, 4);
        }

        for (int i = 0; i < multiNum; i++)
        {
            int tempIndex = Random.Range(0, prefabList.Length);

            GameObject temp = Instantiate(prefabList[tempIndex], transform);

            if (isRotationRandom)
            {
                temp.transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
            }
            
            if (multiNum == 2)
            {
                temp.transform.localPosition = new Vector3(multi2Positions[i].x, 0, multi2Positions[i].y);
            }
            else if (multiNum == 3)
            {
                temp.transform.localPosition = new Vector3(multi3Positions[i].x, 0, multi3Positions[i].y);
            }

            temp.transform.Translate(Vector3.up * 0.03f, Space.Self);
        }
    }
}

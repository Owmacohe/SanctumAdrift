using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleSpawner : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] int multiClusterNum = 1;
    [SerializeField] bool randomClusterNum = true;
    [SerializeField] GameObject[] candleClusterList;

    Vector2[] multiCluster2Positions = { Vector2.one * 0.3f, -Vector2.one * 0.3f };
    Vector2[] multiCluster3Positions = { new Vector2(0, -0.5f), new Vector2(0.5f, 0.5f), new Vector2(-0.5f, 0.5f) };

    void Start()
    {
        if (randomClusterNum)
        {
            multiClusterNum = Random.Range(1, 4);
        }

        for (int i = 0; i < multiClusterNum; i++)
        {
            int candleIndex = Random.Range(0, candleClusterList.Length - 1);

            GameObject candle = Instantiate(candleClusterList[candleIndex], transform);
            candle.transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
            
            if (multiClusterNum == 2)
            {
                candle.transform.localPosition = new Vector3(multiCluster2Positions[i].x, 0, multiCluster2Positions[i].y);
            }
            else if (multiClusterNum == 3)
            {
                candle.transform.localPosition = new Vector3(multiCluster3Positions[i].x, 0, multiCluster3Positions[i].y);
            }

            candle.transform.Translate(Vector3.up * 0.03f, Space.Self);
        }
    }
}

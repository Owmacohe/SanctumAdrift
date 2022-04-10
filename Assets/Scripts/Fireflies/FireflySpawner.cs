using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    [Range(0.1f, 100)]
    [SerializeField] float spawnFrequency = 50;

    BoxCollider box;
    float boxVolume;
    Vector3 halfBoxSize;

    void Start()
    {
        box = GetComponent<BoxCollider>();
        boxVolume = box.bounds.size.x * box.bounds.size.y * box.bounds.size.z;
        halfBoxSize = box.bounds.size / 2;
    }

    void Update()
    {
        if (Random.Range(0, 100) <= spawnFrequency)
        {
            GameObject firefly = Instantiate(Resources.Load<GameObject>("Spawnables/Firefly"), gameObject.transform);
            firefly.GetComponent<FireflyFlutter>().halfSpawnerBoxSize = halfBoxSize;
        }
    }
}

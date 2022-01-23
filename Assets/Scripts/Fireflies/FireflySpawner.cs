using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    [Range(0.1f, 100)]
    public float spawnFrequency = 50;

    private BoxCollider box;
    private float boxVolume;
    private Vector3 halfBoxSize;

    private void Start()
    {
        box = GetComponent<BoxCollider>();
        boxVolume = box.bounds.size.x * box.bounds.size.y * box.bounds.size.z;
        halfBoxSize = box.bounds.size / 2;
    }

    private void Update()
    {
        if (Random.Range(0, 100) <= spawnFrequency)
        {
            GameObject firefly = Instantiate(Resources.Load<GameObject>("Spawnables/Firefly"), gameObject.transform);
            firefly.GetComponent<FireflyFlutter>().halfSpawnerBoxSize = halfBoxSize;
        }
    }
}

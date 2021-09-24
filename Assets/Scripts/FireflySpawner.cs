using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    private BoxCollider box;
    private float boxVolume;

    private void Start()
    {
        box = GetComponent<BoxCollider>();
        boxVolume = box.bounds.size.x * box.bounds.size.y * box.bounds.size.z;
    }

    private void Update()
    {
        Vector3 halfBoxSize = box.bounds.size / 2;

        GameObject firefly = Instantiate(Resources.Load<GameObject>("Firefly"), gameObject.transform);
        firefly.GetComponent<FireflyFlutter>().halfSpawnerBoxSize = halfBoxSize;

        firefly.transform.localPosition = new Vector3(
            Random.Range(-halfBoxSize.x, halfBoxSize.x),
            Random.Range(-halfBoxSize.y, halfBoxSize.y),
            Random.Range(-halfBoxSize.z, halfBoxSize.z)
        );
    }
}

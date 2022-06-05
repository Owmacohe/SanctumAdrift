using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankardSpawner : MonoBehaviour
{
    [Tooltip("The tankard prefab to be spawned")]
    [SerializeField] GameObject tankard;
    [Tooltip("The local rotation amount for the lid of the tankard")]
    [SerializeField] Vector2 lidRotation = new Vector2(30, 160);
    [Tooltip("The materials that can be applied to the tankards")]
    [SerializeField] Material[] materials;
    
    void Start()
    {
        // Creating and rotating the tankard
        GameObject temp = Instantiate(tankard, transform);
        temp.transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));

        // Applying one of the lighter materials to the cup of the tankard
        GameObject cup = temp.transform.GetChild(0).gameObject;
        cup.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length - 1)];
        
        GameObject lid = temp.transform.GetChild(1).gameObject;
        lid.GetComponent<MeshRenderer>().material = materials[Random.Range(1, materials.Length)]; // Applying one of the darker materials to the lid of the tankard
        lid.transform.localRotation = Quaternion.Euler(Vector3.forward * Random.Range(lidRotation.x, lidRotation.y)); // Tilting the tankard lid open
    }
}
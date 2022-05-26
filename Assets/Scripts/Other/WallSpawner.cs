using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallSpawner : MonoBehaviour
{
    [Tooltip("The bank of objects to be picked from to be spawned")]
    [SerializeField] GameObject[] objects;
    [Tooltip("Whether to tilt the spawned objects")]
    [SerializeField] bool tiltObjects = true;
    [Tooltip("The MeshFilter of the wall source")]
    [SerializeField] MeshFilter walls;
    [Tooltip("The number of objects to spawn")]
    [Range(0, 400)] [SerializeField] int density = 200;
    
    void Start()
    {
        Vector3[] vertices = walls.mesh.vertices; // Getting the vertices for future use

        GameObject pipeParent = Instantiate(new GameObject("Pipes"), transform); // Creating a new Empty to house the pipes

        for (int i = 0; i < density; i++)
        {
            int objectIndex = Random.Range(0, objects.Length);
            
            GameObject temp = Instantiate(objects[objectIndex], pipeParent.transform);
            temp.transform.position = walls.transform.TransformPoint(vertices[Random.Range(0, vertices.Length)]); // Placing the pipe at a random vertex
            
            // Making the pipe look inwards
            temp.transform.LookAt(walls.transform);
            temp.transform.localRotation = Quaternion.Euler(new Vector3(0, temp.transform.localRotation.eulerAngles.y, temp.transform.localRotation.eulerAngles.z));

            if (tiltObjects)
            {
                temp.transform.Rotate(temp.transform.forward, Random.Range(-20f, 20f)); // Tilting it slightly
            }
        }
    }
}

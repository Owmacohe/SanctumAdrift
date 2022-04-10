using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAdderSettings : MonoBehaviour
{
    [Tooltip("The only GameObjects to have an outline applied to them")]
    [SerializeField] GameObject[] whitelist;
    [Tooltip("The only GameObjects to *not* have an outline applied to them")]
    [SerializeField] GameObject[] blacklist;

    void Start()
    {
        MeshFilter[] allMeshes = FindObjectsOfType<MeshFilter>(); // Getting the array of all the MeshFilters in the scene

        for (int i = 0; i < allMeshes.Length; i++)
        {
            // Whitelist has first priority...
            if (whitelist.Length > 0)
            {
                for (int j = 0; j < whitelist.Length; j++)
                {
                    if (allMeshes[i].gameObject.Equals(whitelist[j]))
                    {
                        AddOutline(allMeshes[i].gameObject); // Adding an outline to the GameObject if it's on the whitelist
                    }
                }
            }
            // ...then blacklist...
            else if (blacklist.Length > 0)
            {
                bool isOnList = false;

                for (int k = 0; k < blacklist.Length; k++)
                {
                    if (allMeshes[i].gameObject.Equals(blacklist[k]))
                    {
                        isOnList = true;
                        break;
                    }
                }

                if (!isOnList)
                {
                    AddOutline(allMeshes[i].gameObject); // Adding an outline to the GameObject if it's not on the blacklist
                }
            }
            // ...then neither
            else
            {
                AddOutline(allMeshes[i].gameObject); // Adding an outline to each GameObject with a MeshFilter
            }
        }
    }
    
    void AddOutline(GameObject givenGameObject)
    {
        MeshRenderer gameObjectMeshRenderer = givenGameObject.GetComponent<MeshRenderer>();
        Shader gameObjectShader = Resources.Load<Shader>("OutlineMaterial");

        // Making sure that the GameObject doesn't already have an outline
        if (gameObjectMeshRenderer != null && gameObjectMeshRenderer.material.shader != gameObjectShader)
        {
            // Creating the outline
            gameObjectMeshRenderer.material.shader = gameObjectShader;
        }
    }
}

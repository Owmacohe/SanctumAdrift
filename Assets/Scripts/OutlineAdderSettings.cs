/// ----------------------------------------------------------------------
/// Adds outlines to all eligible GameObjects in the scene, with whitelist and blacklist options
/// 
/// @project Sanctum Adrift
/// @version 1.0
/// @organization Lightsea Studio
/// @author Owen Hellum
/// @date September 2021
/// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAdderSettings : MonoBehaviour
{
    [Tooltip("The only GameObjects to have an outline applied to them")]
    public GameObject[] whitelist;
    [Tooltip("The only GameObjects to *not* have an outline applied to them")]
    public GameObject[] blacklist;
    [Tooltip("The colour that each outline should be")]
    public Color outlineColour = Color.black;
    [Tooltip("The width that each outline should be")]
    public float outlineWidth = 7f;

    private void Start()
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
                        addOutline(allMeshes[i].gameObject); // Adding an outline to the GameObject if it's on the whitelist
                    }
                }
            }
            // ...then blacklist...
            else if (blacklist.Length > 0)
            {
                for (int k = 0; k < blacklist.Length; k++)
                {
                    if (!allMeshes[i].gameObject.Equals(blacklist[k]))
                    {
                        addOutline(allMeshes[i].gameObject); // Adding an outline to the GameObject if it's not on the whitelist
                    }
                }
            }
            // ...then neither
            else
            {
                addOutline(allMeshes[i].gameObject); // Adding an outline to each GameObject with a MeshFilter
            }
        }
    }

    /// <summary>
    /// Adds an outline to the the given GameObject
    /// </summary>
    /// <param name="givenGameObject">GameObject to be given an outline</param>
    private void addOutline(GameObject givenGameObject)
    {
        // Making sure that the GameObject doesn't already have an outline
        if (!givenGameObject.GetComponent<Outline>())
        {
            // Creating and setting parameters of the outline
            Outline newOutline = givenGameObject.AddComponent<Outline>();
            newOutline.OutlineMode = Outline.Mode.OutlineVisible;
            newOutline.OutlineColor = outlineColour;
            newOutline.OutlineWidth = outlineWidth;
        }
    }
}

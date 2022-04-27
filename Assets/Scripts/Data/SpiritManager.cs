using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiritManager : MonoBehaviour
{
    [Tooltip("Whether to load the Player's last saved location and rotation at startup")]
    [SerializeField] bool loadPlayerTransformAtStart;
    
    SaveLoadData data; // Script to save and load Characters and Questlines
    
    Transform playerTransform; // Transform of the player GameObject
    Transform cameraTransform; // Transform of the camera GameObject
    
    Player player; // Player class object
    List<Spirit> spiritList; // Spirit class object list

    void Start()
    {
        data = GetComponent<SaveLoadData>();

        if (loadPlayerTransformAtStart)
        {
            player = data.LoadPlayer(0);
        }
        // If not loading, the Player file is emptied
        else
        {
            player = new Player();
            data.Empty("player_data.txt");
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cameraTransform = Camera.main.transform.parent;
        
        // TODO: camera position gets weird when loading zoomed in position
        
        // Assigning the player and camera transforms
        playerTransform.position = player.PlayerPosition;
        playerTransform.rotation = Quaternion.Euler(player.PlayerRotation);
        //cameraTransform.position = player.CameraPosition;
        //cameraTransform.rotation = Quaternion.Euler(player.CameraRotation);

        /*
        spiritList = new List<Spirit>();

        foreach (Spirit.SpiritTypes i in Enum.GetValues(typeof(Spirit.SpiritTypes)))
        {
            if (!i.Equals(Spirit.SpiritTypes.None))
            {
                spiritList.Add(new Spirit(GenerateName(), Spirit.SpiritClasses.Magnanimous, i));

                for (int maj = 0; maj < 4; maj++)
                {
                    spiritList.Add(new Spirit(GenerateName(), Spirit.SpiritClasses.Major, i));
                }

                for (int med = 0; med < 10; med++)
                {
                    spiritList.Add(new Spirit(GenerateName(), Spirit.SpiritClasses.Median, i));
                }

                for (int min = 0; min < 5; min++)
                {
                    spiritList.Add(new Spirit(GenerateName(), Spirit.SpiritClasses.Minor, i));
                }
            }
        }
        
        data.SaveSpirits(spiritList);
        */
    }

    void FixedUpdate()
    {
        // Saving the Player every 2 seconds
        if (Time.time % 2 == 0 && Time.time >= 2)
        {
            SavePlayerTransform();
        }
    }

    /// <summary>
    /// Saves the player and camera Transforms to the Player file
    /// </summary>
    void SavePlayerTransform()
    {
        player.SetPlayerPosition(playerTransform.position);
        player.SetPlayerRotation(playerTransform.rotation.eulerAngles);
        player.SetCameraPosition(cameraTransform.position);
        player.SetCameraRotation(cameraTransform.rotation.eulerAngles);
        
        data.SavePlayer(player);
    }

    /// <summary>
    /// Creates a random first name from a list of prefixes and suffixes
    /// </summary>
    /// <returns></returns>
    string GenerateName()
    {
        string[] NPCNamePrefixes = {
            "Gor",
            "Blez",
            "Zyhaph",
            "Bob",
            "Eg",
            "Will",
            "Suz",
            "Lill",
            "Jos",
            "Dave",
            "Orm",
            "Zeggl",
            "Son",
            "Yog"
        };
    
        string[] NPCNameSuffixes = {
            "go",
            "omat",
            "omel",
            "bert",
            "iam",
            "anna",
            "ith",
            "iah",
            "onath",
            "etor",
            "ia",
            "othor"
        };
        
        return NPCNamePrefixes[Random.Range(0, NPCNamePrefixes.Length)] + 
               NPCNameSuffixes[Random.Range(0, NPCNameSuffixes.Length)];
    }
}

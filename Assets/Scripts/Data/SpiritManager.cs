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
            
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            cameraTransform = Camera.main.transform.parent;
        
            // TODO: camera position gets weird when loading zoomed in position
        
            // Assigning the player and camera transforms
            playerTransform.position = player.PlayerPosition;
            playerTransform.rotation = Quaternion.Euler(player.PlayerRotation);
            //cameraTransform.position = player.CameraPosition;
            //cameraTransform.rotation = Quaternion.Euler(player.CameraRotation);
        }
        // If not loading, the Player file is emptied
        else
        {
            player = new Player();
            data.Empty("player_data.txt");
        }
        
        /*
        spiritList = new List<Spirit>();

        foreach (Spirit.SpiritTypes i in Enum.GetValues(typeof(Spirit.SpiritTypes)))
        {
            if (!i.Equals(Spirit.SpiritTypes.None))
            {
                spiritList.Add(new Spirit(SpiritName.Generate(), Spirit.SpiritClasses.Magnanimous, i));

                for (int maj = 0; maj < 4; maj++)
                {
                    spiritList.Add(new Spirit(SpiritName.Generate(), Spirit.SpiritClasses.Major, i));
                }

                for (int med = 0; med < 10; med++)
                {
                    spiritList.Add(new Spirit(SpiritName.Generate(), Spirit.SpiritClasses.Median, i));
                }

                for (int min = 0; min < 5; min++)
                {
                    spiritList.Add(new Spirit(SpiritName.Generate(), Spirit.SpiritClasses.Minor, i));
                }
            }
        }
        
        data.SaveSpirits(spiritList);
        */
    }

    void FixedUpdate()
    {
        // Saving the Player every 2 seconds
        if (loadPlayerTransformAtStart && Time.time % 2 == 0 && Time.time >= 2)
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
}

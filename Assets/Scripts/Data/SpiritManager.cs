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
            player = data.LoadPlayer("player_data.txt");
            
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
            data.SavePlayer(player, "player_data.txt");
        }
        
        GenerateSpiritList();
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
        
        data.SavePlayer(player, "player_data.txt");
    }

    /// <summary>
    /// Creates a List of 100 mostly new and random Spirits to inhabit the game
    /// (custom pre-created Spirits have priority over randomly-generated ones)
    /// </summary>
    void GenerateSpiritList()
    {
        // The integer counts of how many Spirits of each type and class should be created
        int[,] spiritCounts =
        {
            { 1, 4, 10, 5 }, // Leaf
            { 1, 4, 10, 5 }, // Liquor
            { 1, 4, 10, 5 }, // Paper
            { 1, 4, 10, 5 }, // Ember
            { 1, 4, 10, 5 }  // Bone
        };
        
        // Initializing the empty and custom lists
        spiritList = new List<Spirit>();
        List<Spirit> temp = data.LoadSpirits("custom_spirits.txt");

        // Making sure there are in fact custom Spirits to be loaded
        if (temp != null)
        {
            // Looping through them
            foreach (Spirit i in temp)
            {
                // Making sure its type and class aren't none
                if (!i.SpiritType.Equals(Spirit.SpiritTypes.None) && !i.SpiritClass.Equals(Spirit.SpiritClasses.None))
                {
                    int typeIndex = 0;
                    int classIndex = 0;
            
                    // Determining the custom Spirit's type index
                    switch (i.SpiritType)
                    {
                        case Spirit.SpiritTypes.Leaf:
                            typeIndex = 0;
                            break;
                        case Spirit.SpiritTypes.Liquor:
                            typeIndex = 1;
                            break;
                        case Spirit.SpiritTypes.Paper:
                            typeIndex = 2;
                            break;
                        case Spirit.SpiritTypes.Ember:
                            typeIndex = 3;
                            break;
                        case Spirit.SpiritTypes.Bone:
                            typeIndex = 4;
                            break;
                    }
            
                    // Determining the custom Spirit's class index
                    switch (i.SpiritClass)
                    {
                        case Spirit.SpiritClasses.Magnanimous:
                            classIndex = 0;
                            break;
                        case Spirit.SpiritClasses.Major:
                            classIndex = 1;
                            break;
                        case Spirit.SpiritClasses.Median:
                            classIndex = 2;
                            break;
                        case Spirit.SpiritClasses.Minor:
                            classIndex = 3;
                            break;
                    }

                    spiritCounts[typeIndex, classIndex]--; // Decreasing the count at that index (so it won't be randomly generated later)
                
                    spiritList.Add(i); // Adding it to the Spirit list
                }
            }
        }

        int jIndex = 0; // Type index for the random Spirits
        
        // Creating random Spirits for each type
        foreach (Spirit.SpiritTypes j in Enum.GetValues(typeof(Spirit.SpiritTypes)))
        {
            // Making sure its type isn't none
            if (!j.Equals(Spirit.SpiritTypes.None))
            {
                // Generating the required amount of Magnanimous Spirits for the current type
                for (int mag = 0; mag < spiritCounts[jIndex, 0]; mag++)
                {
                    spiritList.Add(new Spirit(RandomSpiritName.Generate(), Spirit.SpiritClasses.Magnanimous, j));
                }

                // Generating the required amount of Major Spirits for the current type
                for (int maj = 0; maj < spiritCounts[jIndex, 1]; maj++)
                {
                    spiritList.Add(new Spirit(RandomSpiritName.Generate(), Spirit.SpiritClasses.Major, j));
                }

                // Generating the required amount of Median Spirits for the current type
                for (int med = 0; med < spiritCounts[jIndex, 2]; med++)
                {
                    spiritList.Add(new Spirit(RandomSpiritName.Generate(), Spirit.SpiritClasses.Median, j));
                }

                // Generating the required amount of Minor Spirits for the current type
                for (int min = 0; min < spiritCounts[jIndex, 3]; min++)
                {
                    spiritList.Add(new Spirit(RandomSpiritName.Generate(), Spirit.SpiritClasses.Minor, j));
                }

                jIndex++;
            }
        }
        
        data.SaveSpirits(spiritList, "spirit_data.txt"); // Saving the new Spirit list to a file
    }
}

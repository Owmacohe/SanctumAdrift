using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiritManager : MonoBehaviour
{
    [SerializeField] bool loadPlayerTransformAtStart;
    
    SaveLoadData data;
    
    Player player;
    Transform playerTransform;
    Transform cameraTransform;
    
    List<Spirit> spiritList;

    void Start()
    {
        data = GetComponent<SaveLoadData>();

        if (loadPlayerTransformAtStart)
        {
            player = data.LoadPlayer("player_data.txt", 0);
        }
        else
        {
            player = new Player();
            data.Empty("player_data.txt");
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cameraTransform = Camera.main.transform.parent;
        
        // TODO: camera position gets weird when loading zoomed in position
        
        playerTransform.position = player.PlayerPosition;
        playerTransform.rotation = Quaternion.Euler(player.PlayerRotation);
        cameraTransform.position = player.CameraPosition;
        cameraTransform.rotation = Quaternion.Euler(player.CameraRotation);
        
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
        if (Time.time % 2 == 0 && Time.time >= 2)
        {
            SavePlayerTransform();
        }
    }

    void SavePlayerTransform()
    {
        player.SetPlayerPosition(playerTransform.position);
        player.SetPlayerRotation(playerTransform.rotation.eulerAngles);
        player.SetCameraPosition(cameraTransform.position);
        player.SetCameraRotation(cameraTransform.rotation.eulerAngles);
        
        data.SavePlayer(player);
    }

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

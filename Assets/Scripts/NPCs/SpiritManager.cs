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
    
    List<NPC> NPCList;

    void Start()
    {
        data = GetComponent<SaveLoadData>();

        if (loadPlayerTransformAtStart)
        {
            player = data.LoadPlayer();
        }
        else
        {
            player = new Player();
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cameraTransform = Camera.main.transform.parent;
        
        playerTransform.position = player.PlayerPosition;
        playerTransform.rotation = Quaternion.Euler(player.PlayerRotation);
        cameraTransform.position = player.CameraPosition;
        cameraTransform.rotation = Quaternion.Euler(player.CameraRotation);
        
        /*
        NPCList = new List<NPC>();

        foreach (Spirit.SpiritTypes i in Enum.GetValues(typeof(Spirit.SpiritTypes)))
        {
            if (!i.Equals(Spirit.SpiritTypes.None))
            {
                NPCList.Add(new NPC(GenerateName(), Spirit.SpiritClasses.Magnanimous, i));

                for (int maj = 0; maj < 4; maj++)
                {
                    NPCList.Add(new NPC(GenerateName(), Spirit.SpiritClasses.Major, i));
                }

                for (int med = 0; med < 10; med++)
                {
                    NPCList.Add(new NPC(GenerateName(), Spirit.SpiritClasses.Median, i));
                }

                for (int min = 0; min < 5; min++)
                {
                    NPCList.Add(new NPC(GenerateName(), Spirit.SpiritClasses.Minor, i));
                }
            }
        }
        
        data.SaveNPCs(NPCList);
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

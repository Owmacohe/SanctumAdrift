using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiritManager : MonoBehaviour
{
    Player player;
    List<NPC> NPCList;

    void Start()
    {
        player = new Player("Player Name", Spirit.SpiritClasses.None, Spirit.SpiritTypes.None);
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

        SaveLoadData temp = GetComponent<SaveLoadData>();
        temp.SavePlayer(player);
        temp.SaveNPCs(NPCList);
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

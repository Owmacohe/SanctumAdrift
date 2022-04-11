using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    string path;

    void Start()
    {
        path = Application.dataPath + "/Resources/Data/";
    }

    public void SavePlayer(Player player)
    {
        string temp = "player_data.txt";
        
        Empty(temp);
        
        Save(temp, player.StringValue());
    }

    public void SaveNPCs(List<NPC> NPCList)
    {
        string temp = "npc_data.txt";
        
        Empty(temp);
        
        foreach (NPC i in NPCList)
        {
            Save(temp, i.StringValue());
        }
    }

    public Player LoadPlayer()
    {
        Spirit spirit = LoadSpirit("player_data.txt", 0);
        Player temp = new Player(spirit.Name, spirit.SpiritClass, spirit.SpiritType);
        
        string[] lines = Load("player_data.txt");

        for (int i = 3; i < lines.Length; i++)
        {
            string[] split = lines[i].Split(' ');
            
            temp.Questlines.Add(new Spirit(split[0], Spirit.SpiritClasses.None, Spirit.SpiritTypes.None), int.Parse(split[1]));
        }
        
        return temp;
    }

    Spirit LoadSpirit(string fileName, int offset)
    {
        string[] lines = Load("player_data.txt");

        Spirit temp = new Spirit();
        temp.Name = lines[offset];

        Spirit.SpiritClasses sClass = Spirit.SpiritClasses.None;

        switch (lines[offset + 1])
        {
            case "None":
                sClass = Spirit.SpiritClasses.None;
                break;
            case "Minor":
                sClass = Spirit.SpiritClasses.Minor;
                break;
            case "Median":
                sClass = Spirit.SpiritClasses.Median;
                break;
            case "Major":
                sClass = Spirit.SpiritClasses.Major;
                break;
            case "Magnanimous":
                sClass = Spirit.SpiritClasses.Magnanimous;
                break;
        }

        temp.SpiritClass = sClass;
        
        Spirit.SpiritTypes sType = Spirit.SpiritTypes.None;

        switch (lines[offset + 2])
        {
            case "None":
                sType = Spirit.SpiritTypes.None;
                break;
            case "Leaf":
                sType = Spirit.SpiritTypes.Leaf;
                break;
            case "Liquor":
                sType = Spirit.SpiritTypes.Liquor;
                break;
            case "Paper":
                sType = Spirit.SpiritTypes.Paper;
                break;
            case "Ember":
                sType = Spirit.SpiritTypes.Ember;
                break;
            case "Bone":
                sType = Spirit.SpiritTypes.Bone;
                break;
        }

        temp.SpiritType = sType;

        return temp;
    }

    public void Empty(string fileName)
    {
        File.WriteAllText(path + fileName, "");
    }

    void Save(string fileName, string line)
    {
        string temp = "";

        try
        {
            if (File.ReadAllLines(path + fileName).Length != 0)
            {
                temp += "\n";
            }
        }
        catch { }

        temp += line;
        
        File.AppendAllText(path + fileName, temp);
        
        AssetDatabase.Refresh();
    }

    string[] Load(string fileName)
    {
        try
        {
            return File.ReadAllLines(path + fileName);
        }
        catch
        {
            return null;
        }
    }
}
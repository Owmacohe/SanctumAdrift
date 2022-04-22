using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveLoadData : MonoBehaviour // TODO: a lot of this needs to redone with new class changes
{
    string path;

    void Start()
    {
        CheckPath();
    }

    void CheckPath()
    {
        if (path == null)
        {
            path = Application.dataPath + "/Resources/Data/";
        }
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
        string name = "player_data.txt";
        
        Spirit spirit = LoadSpirit(name, 0);

        if (spirit != null)
        {
            Player temp = new Player(spirit.Name, spirit.SpiritClass, spirit.SpiritType);
        
            string[] lines = Load(name);

            string[] positionSplit = lines[3].Trim().Split(' ');
            temp.SetPlayerPosition(new Vector3(float.Parse(positionSplit[0]), float.Parse(positionSplit[1]), float.Parse(positionSplit[2])));
            string[] rotationSplit = lines[4].Trim().Split(' ');
            temp.SetPlayerRotation(new Vector3(float.Parse(rotationSplit[0]), float.Parse(rotationSplit[1]), float.Parse(rotationSplit[2])));
            string[] cameraPositionSplit = lines[5].Trim().Split(' ');
            temp.SetCameraRotation(new Vector3(float.Parse(cameraPositionSplit[0]), float.Parse(cameraPositionSplit[1]), float.Parse(cameraPositionSplit[2])));
            string[] cameraRotationSplit = lines[6].Trim().Split(' ');
            temp.SetCameraRotation(new Vector3(float.Parse(cameraRotationSplit[0]), float.Parse(cameraRotationSplit[1]), float.Parse(cameraRotationSplit[2])));

            for (int i = 7; i < lines.Length; i++)
            {
                string[] split = lines[i].Trim().Split(' ');
            
                temp.Questlines.Add(split[0], int.Parse(split[1]));
            }
        
            return temp;
        }
        else
        {
            Empty(name);

            Player temp = new Player();
            SavePlayer(temp);

            return temp;
        }
    }
    
    public List<NPC> LoadNPCs()
    {
        string name = "npc_data.txt";
        
        string[] lines = Load(name);
        List<NPC> temp = new List<NPC>();

        if (lines != null)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().Equals("---"))
                {
                    Spirit spirit = LoadSpirit(name, i + 1);
                
                    NPC tempNPC = new NPC(spirit.Name, spirit.SpiritClass, spirit.SpiritType);

                    int j = i + 4;

                    while (j < lines.Length && !lines[j].Trim().Equals("---"))
                    {
                        string[] split = lines[j].Trim().Split(' ');
                    
                        tempNPC.Opinions.Add(split[0], int.Parse(split[1]));

                        j++;
                    }
                
                    temp.Add(tempNPC);
                }
            }
        }
        else
        {
            Empty(name);
        }
        
        return temp;
    }

    Spirit LoadSpirit(string fileName, int offset)
    {
        string[] lines = Load(fileName);

        if (lines != null)
        {
            Spirit temp = new Spirit();
            
            temp.Name = lines[offset].Trim();

            Spirit.SpiritClasses sClass = Spirit.SpiritClasses.None;

            switch (lines[offset + 1].Trim())
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

            switch (lines[offset + 2].Trim())
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
        
        return null;
    }

    public void Empty(string fileName)
    {
        CheckPath();
        File.WriteAllText(path + fileName, "");
    }

    void Save(string fileName, string line)
    {
        CheckPath();
        
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
        CheckPath();
        
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
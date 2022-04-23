using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
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
    
    public void Empty(string fileName)
    {
        CheckPath();
        File.WriteAllText(path + fileName, "");
    }

    void SaveLine(string fileName, string line)
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

    public void SavePlayer(Player character)
    {
        string temp = "player_data.txt";
        Empty(temp);
        
        SaveLine(temp, character.StringValue());
    }
    
    public void SaveSpirits(List<Spirit> spirits)
    {
        string temp = "spirit_data.txt";
        Empty(temp);
        
        foreach (Spirit i in spirits)
        {
            SaveLine(temp, i.StringValue());
        }
    }
    
    public void SaveQuestlines(List<Questline> questlines, string fileName)
    {
        string temp = fileName;
        Empty(temp);
        
        foreach (Questline i in questlines)
        {
            SaveLine(temp, i.StringValue());
        }
    }
    
    public void SaveQuestlineNodes(List<QuestlineNode> questlineNodes, string fileName)
    {
        string temp = fileName;
        Empty(temp);
        
        foreach (QuestlineNode i in questlineNodes)
        {
            SaveLine(temp, i.StringValue());
        }
    }
    
    string[] LoadLines(string fileName)
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

    public Character LoadCharacter(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        
        string[] lines = LoadLines(fileName);

        if (lines != null && lines[offset].Trim().Equals("[CHARACTER]"))
        {
            return new Character(lines[++offset].Trim());
        }

        return null;
    }

    public NPC LoadNPC(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        // [NPC]
        // questline name
        // opinions ...

        Character tempCharacter = LoadCharacter(fileName, offset);

        if (tempCharacter != null)
        {
            string[] lines = LoadLines(fileName);
            
            NPC temp = new NPC(tempCharacter.Name);
            
            while (!lines[++offset].Trim().Equals("[NPC]")) { }

            temp.Questline = new Questline(lines[++offset].Trim());
            
            while (!lines[++offset].Trim().Equals("[SPIRIT]") && !lines[offset].Trim().Equals("[ADVENTURER]") && !lines[offset].Trim().Equals("[CHARACTER]"))
            {
                string[] split = lines[offset].Trim().Split(' ');
                
                temp.Opinions.Add(split[0], int.Parse(split[1]));
            }

            return temp;
        }

        return null;
    }

    public Spirit LoadSpirit(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        // [NPC]
        // questline name
        // opinions ...
        // [SPIRIT]
        // spirit class
        // spirit type
        
        NPC tempNPC = LoadNPC(fileName, offset);

        if (tempNPC != null)
        {
            string[] lines = LoadLines(fileName);
            
            Spirit temp = new Spirit(tempNPC.Name);
            temp.Questline = tempNPC.Questline;
            temp.Opinions = tempNPC.Opinions;
            
            while (!lines[++offset].Trim().Equals("[SPIRIT]")) { }
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    temp.SpiritClass = Spirit.SpiritClasses.None;
                    break;
                case "Minor":
                    temp.SpiritClass = Spirit.SpiritClasses.Minor;
                    break;
                case "Median":
                    temp.SpiritClass = Spirit.SpiritClasses.Median;
                    break;
                case "Major":
                    temp.SpiritClass = Spirit.SpiritClasses.Major;
                    break;
                case "Magnanimous":
                    temp.SpiritClass = Spirit.SpiritClasses.Magnanimous;
                    break;
            }
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    temp.SpiritType = Spirit.SpiritTypes.None;
                    break;
                case "Leaf":
                    temp.SpiritType = Spirit.SpiritTypes.Leaf;
                    break;
                case "Liquor":
                    temp.SpiritType = Spirit.SpiritTypes.Liquor;
                    break;
                case "Paper":
                    temp.SpiritType = Spirit.SpiritTypes.Paper;
                    break;
                case "Ember":
                    temp.SpiritType = Spirit.SpiritTypes.Ember;
                    break;
                case "Bone":
                    temp.SpiritType = Spirit.SpiritTypes.Bone;
                    break;
            }

            return temp;
        }

        return null;
    }

    public Adventurer LoadAdventurer(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        // [NPC]
        // questline name
        // opinions ...
        // [ADVENTURER]
        // TODO: attributes
        
        NPC tempNPC = LoadNPC(fileName, offset);

        if (tempNPC != null)
        {
            Adventurer temp = new Adventurer(tempNPC.Name);
            temp.Questline = tempNPC.Questline;
            temp.Opinions = tempNPC.Opinions;

            return temp;
        }

        return null;
    }

    public Player LoadPlayer(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        // [PLAYER]
        // TODO: settings
        // TODO: inventory
        // TODO: cosmetic items
        // spirit class
        // spirit type
        // player position
        // player rotation
        // camera position
        // camera rotation
        // questlines ...
        
        Character tempCharacter = LoadCharacter(fileName, offset);

        if (tempCharacter != null)
        {
            string[] lines = LoadLines(fileName);

            Player temp = new Player(tempCharacter.Name);

            while (!lines[++offset].Trim().Equals("[PLAYER]")) { }
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    temp.SpiritClass = Spirit.SpiritClasses.None;
                    break;
                case "Minor":
                    temp.SpiritClass = Spirit.SpiritClasses.Minor;
                    break;
                case "Median":
                    temp.SpiritClass = Spirit.SpiritClasses.Median;
                    break;
                case "Major":
                    temp.SpiritClass = Spirit.SpiritClasses.Major;
                    break;
                case "Magnanimous":
                    temp.SpiritClass = Spirit.SpiritClasses.Magnanimous;
                    break;
            }
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    temp.SpiritType = Spirit.SpiritTypes.None;
                    break;
                case "Leaf":
                    temp.SpiritType = Spirit.SpiritTypes.Leaf;
                    break;
                case "Liquor":
                    temp.SpiritType = Spirit.SpiritTypes.Liquor;
                    break;
                case "Paper":
                    temp.SpiritType = Spirit.SpiritTypes.Paper;
                    break;
                case "Ember":
                    temp.SpiritType = Spirit.SpiritTypes.Ember;
                    break;
                case "Bone":
                    temp.SpiritType = Spirit.SpiritTypes.Bone;
                    break;
            }
            
            string[] playerPositionSplit = lines[++offset].Trim().Split(' ');
            temp.SetPlayerPosition(new Vector3(float.Parse(playerPositionSplit[0]), float.Parse(playerPositionSplit[1]), float.Parse(playerPositionSplit[2])));
            string[] playerRotationSplit = lines[++offset].Trim().Split(' ');
            temp.SetPlayerRotation(new Vector3(float.Parse(playerRotationSplit[0]), float.Parse(playerRotationSplit[1]), float.Parse(playerRotationSplit[2])));
            
            string[] cameraPositionSplit = lines[++offset].Trim().Split(' ');
            temp.SetCameraRotation(new Vector3(float.Parse(cameraPositionSplit[0]), float.Parse(cameraPositionSplit[1]), float.Parse(cameraPositionSplit[2])));
            string[] cameraRotationSplit = lines[++offset].Trim().Split(' ');
            temp.SetCameraRotation(new Vector3(float.Parse(cameraRotationSplit[0]), float.Parse(cameraRotationSplit[1]), float.Parse(cameraRotationSplit[2])));

            while (!lines[++offset].Trim().Equals("[CHARACTER]"))
            {
                temp.Questlines.Add(lines[offset].Trim());
            }

            return temp;
        }

        return null;
    }

    public Questline LoadQuestline(string fileName, int offset)
    {
        // [QUESTLINE]
        // name
        // questline type
        // questline state
        // TODO: requirements
        // TODO: rewards
        // nodes ...
        
        string[] lines = LoadLines(fileName);

        if (lines != null && lines[offset].Trim().Equals("[QUESTLINE]"))
        {
            Questline temp = new Questline(lines[++offset].Trim());
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    temp.QuestlineType = Questline.QuestlineTypes.None;
                    break;
            }
            
            switch (lines[++offset].Trim())
            {
                case "Unstarted":
                    temp.QuestlineState = Questline.QuestlineStates.Unstarted;
                    break;
                case "Started":
                    temp.QuestlineState = Questline.QuestlineStates.Started;
                    break;
                case "Finished":
                    temp.QuestlineState = Questline.QuestlineStates.Finished;
                    break;
            }

            while (!lines[++offset].Trim().Equals("[QUESTLINE]"))
            {
                string[] nodeSplit = lines[offset].Trim().Split(' ');
                
                temp.Nodes.Add(nodeSplit[0]);

                if (nodeSplit.Length > 1 && nodeSplit[1].Equals("(current)"))
                {
                    temp.CurrentNode = nodeSplit[1];
                }
            }

            return temp;
        }

        return null;
    }

    public QuestlineNode LoadQuestlineNode(string fileName, int offset)
    {
        // [NODE]
        // name
        // questline name
        // TODO: requirements
        // previous name
        // next name
        
        string[] lines = LoadLines(fileName);

        if (lines != null && lines[offset].Trim().Equals("[NODE]"))
        {
            return new QuestlineNode(
                lines[++offset].Trim(), 
                lines[++offset].Trim(), 
                lines[++offset].Trim(), 
                lines[++offset].Trim()
            );
        }

        return null;
    }
}
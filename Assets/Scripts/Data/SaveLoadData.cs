using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    string path; // Local data path to the Resources Data folder

    void Start()
    {
        CheckPath();
    }

    /// <summary>
    /// Makes sure that the path isn't null
    /// </summary>
    void CheckPath()
    {
        if (path == null)
        {
            path = Application.dataPath + "/Resources/Data/";
        }
    }
    
    /// <summary>
    /// Empties a given file, making it blank
    /// </summary>
    /// <param name="fileName">Path-less name of the target file</param>
    public void Empty(string fileName)
    {
        CheckPath();
        File.WriteAllText(path + fileName, "");
    }

    /// <summary>
    /// Appends a given line to a given file
    /// </summary>
    /// <param name="fileName">Path-less name of the target file</param>
    /// <param name="line">New lines to be appended</param>
    void SaveLine(string fileName, string line)
    {
        CheckPath();
        
        string temp = "";

        try
        {
            // Making sure to add the new line as a new line
            // instead of just at the end of the last line
            if (File.ReadAllLines(path + fileName).Length != 0)
            {
                temp += "\n";
            }
        }
        catch { }

        temp += line;
        
        File.AppendAllText(path + fileName, temp); // Appending the new line
        
        AssetDatabase.Refresh(); // Allowing the file to be immediately accessible
    }

    /// <summary>
    /// Re-writing the Spirits in their file
    /// </summary>
    /// <param name="spirits">List of Spirits to be written</param>
    public void SaveSpirits(List<Spirit> spirits)
    {
        string temp = "spirit_data.txt";
        Empty(temp);
        
        foreach (Spirit i in spirits)
        {
            SaveLine(temp, i.StringValue());
        }
    }
    
    /// <summary>
    /// Re-writing the Adventurers in their file
    /// </summary>
    /// <param name="adventurers">List of Adventurers to be written</param>
    public void SaveAdventurers(List<Adventurer> adventurers)
    {
        string temp = "adventurer_data.txt";
        Empty(temp);
        
        foreach (Adventurer i in adventurers)
        {
            SaveLine(temp, i.StringValue());
        }
    }
    
    /// <summary>
    /// Re-writing the Player in its file
    /// </summary>
    /// <param name="player">Player to be written</param>
    public void SavePlayer(Player player)
    {
        string temp = "player_data.txt";
        Empty(temp);
        
        SaveLine(temp, player.StringValue());
    }
    
    /// <summary>
    /// Gets all the lines from a given file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Path-less name of the target file</param>
    /// <returns>String array of lines from the file</returns>
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

    /// <summary>
    /// Extracts a Character object from a given text file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Path-less name of the target file</param>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted Character</returns>
    public Character LoadCharacter(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        
        string[] lines = LoadLines(fileName);

        // Making sure that the file exists and that a Character starts at this offset
        if (lines != null && lines[offset].Trim().Equals("[CHARACTER]"))
        {
            return new Character(lines[++offset].Trim());
        }

        return null;
    }

    /// <summary>
    /// Extracts a NPC object from a given text file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Path-less name of the target file</param>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted NPC</returns>
    public NPC LoadNPC(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        // [NPC]
        // questline name
        // opinions ...

        Character tempCharacter = LoadCharacter(fileName, offset);

        // Making sure that the file exists and that a Character starts at this offset
        if (tempCharacter != null)
        {
            string[] lines = LoadLines(fileName);
            
            NPC temp = new NPC(tempCharacter.Name);
            
            while (!lines[++offset].Trim().Equals("[NPC]")) { } // Skipping until the NPC basic attributes

            temp.Questline = lines[++offset].Trim();
            
            // Skipping over anything that's not a NPC opinion (complex attributes)
            while (offset + 1 < lines.Length && !lines[++offset].Trim().Equals("[SPIRIT]") && !lines[offset].Trim().Equals("[ADVENTURER]") && !lines[offset].Trim().Equals("[CHARACTER]"))
            {
                string[] split = lines[offset].Trim().Split(' ');
                
                temp.Opinions.Add(split[0], int.Parse(split[1]));
            }

            return temp;
        }

        return null;
    }

    /// <summary>
    /// Extracts a Spirit object from the Spirit file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted Spirit</returns>
    public Spirit LoadSpirit(int offset)
    {
        // [CHARACTER]
        // name
        // [NPC]
        // questline name
        // opinions ...
        // [SPIRIT]
        // spirit class
        // spirit type

        string fileName = "spirit_data.txt";
        NPC tempNPC = LoadNPC(fileName, offset);

        // Making sure that the file exists and that a Character starts at this offset
        if (tempNPC != null)
        {
            string[] lines = LoadLines(fileName);
            
            Spirit temp = new Spirit(tempNPC.Name);
            temp.Questline = tempNPC.Questline;
            temp.Opinions = tempNPC.Opinions;
            
            while (!lines[++offset].Trim().Equals("[SPIRIT]")) { } // Skipping until the Spirit basic attributes
            
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

    /// <summary>
    /// Extracts an Adventurer object from the Adventurer file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted Adventurer</returns>
    public Adventurer LoadAdventurer(int offset)
    {
        // [CHARACTER]
        // name
        // [NPC]
        // questline name
        // opinions ...
        // [ADVENTURER]
        // TODO: attributes

        string fileName = "adventurer_data.txt";
        NPC tempNPC = LoadNPC(fileName, offset);

        // Making sure that the file exists and that a Character starts at this offset
        if (tempNPC != null)
        {
            Adventurer temp = new Adventurer(tempNPC.Name);
            temp.Questline = tempNPC.Questline;
            temp.Opinions = tempNPC.Opinions;

            return temp;
        }

        return null;
    }

    /// <summary>
    /// Extracts a Player object from the Player file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted Player</returns>
    public Player LoadPlayer(int offset)
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

        string fileName = "player_data.txt";
        Character tempCharacter = LoadCharacter(fileName, offset);

        // Making sure that the file exists and that a Character starts at this offset
        if (tempCharacter != null)
        {
            string[] lines = LoadLines(fileName);

            Player temp = new Player(tempCharacter.Name);

            while (!lines[++offset].Trim().Equals("[PLAYER]")) { } // Skipping until the Player basic attributes
            
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

            // Skipping over anything that's not a Player questline (complex attributes)
            while (offset + 1 < lines.Length && !lines[++offset].Trim().Equals("[CHARACTER]"))
            {
                temp.Questlines.Add(lines[offset].Trim());
            }

            return temp;
        }

        return null;
    }
}
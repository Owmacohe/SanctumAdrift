using System;
using System.Collections.Generic;
using System.IO;
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
    /// <param name="fileName">Local file to write data to</param>
    public void SaveSpirits(List<Spirit> spirits, string fileName)
    {
        string temp = fileName;
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
    /// <param name="fileName">Local file to write data to</param>
    public void SaveAdventurers(List<Adventurer> adventurers, string fileName)
    {
        string temp = fileName;
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
    /// <param name="fileName">Local file to write data to</param>
    public void SavePlayer(Player player, string fileName)
    {
        string temp = fileName;
        Empty(temp);
        
        SaveLine(temp, player.StringValue());
    }
    
    /// <summary>
    /// Gets all the lines from a given file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
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
    /// Abstract helper method to loop through a file, loading each Character with the specified loader method
    /// (each Character in the file *must* be loadable by the specified individual loader)
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <param name="IndividualLoader">Loader method from which to load a derived Character at a given offset</param>
    /// <typeparam name="T">Derived Character class of the Characters being loaded</typeparam>
    /// <returns>A list of the specified derived Character objects, all loaded from the file</returns>
    List<T> LoadMultiples<T>(string fileName, Func<string, int, T> IndividualLoader) where T: new()
    {
        string[] lines = LoadLines(fileName);

        if (lines != null)
        {
            List<T> temp = new List<T>();

            for (int i = 0; i < lines.Length; i++)
            {
                // Stopping when we hit a Character of any type
                if (lines[i].Trim().Equals("[CHARACTER]"))
                {
                    temp.Add(IndividualLoader(fileName, i)); // Adding the loaded derived Character object to the list
                }
            }

            // Only returning the list if it's non-empty
            if (temp.Count > 0)
            {
                return temp;
            }
        }
        
        return null;
    }

    /// <summary>
    /// Extracts a Character object from a given text file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
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
    /// Method to load all the Characters from a given file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <returns>A List of Characters from the file</returns>
    public List<Character> LoadCharacters(string fileName) { return LoadMultiples(fileName, LoadCharacter); }

    /// <summary>
    /// Extracts a NPC object from a given text file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
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
    /// Method to load all the NPCs from a given file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <returns>A List of NPCs from the file</returns>
    public List<NPC> LoadNPCs(string fileName) { return LoadMultiples(fileName, LoadNPC); }

    /// <summary>
    /// Extracts a Spirit object from the Spirit file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted Spirit</returns>
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

        string temp = fileName;
        NPC tempNPC = LoadNPC(temp, offset);

        // Making sure that the file exists and that a Character starts at this offset
        if (tempNPC != null)
        {
            string[] lines = LoadLines(temp);
            
            Spirit tempSpirit = new Spirit(tempNPC.Name);
            tempSpirit.Questline = tempNPC.Questline;
            tempSpirit.Opinions = tempNPC.Opinions;
            
            while (!lines[++offset].Trim().Equals("[SPIRIT]")) { } // Skipping until the Spirit basic attributes
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    tempSpirit.SpiritClass = Spirit.SpiritClasses.None;
                    break;
                case "Minor":
                    tempSpirit.SpiritClass = Spirit.SpiritClasses.Minor;
                    break;
                case "Median":
                    tempSpirit.SpiritClass = Spirit.SpiritClasses.Median;
                    break;
                case "Major":
                    tempSpirit.SpiritClass = Spirit.SpiritClasses.Major;
                    break;
                case "Magnanimous":
                    tempSpirit.SpiritClass = Spirit.SpiritClasses.Magnanimous;
                    break;
            }
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    tempSpirit.SpiritType = Spirit.SpiritTypes.None;
                    break;
                case "Leaf":
                    tempSpirit.SpiritType = Spirit.SpiritTypes.Leaf;
                    break;
                case "Liquor":
                    tempSpirit.SpiritType = Spirit.SpiritTypes.Liquor;
                    break;
                case "Paper":
                    tempSpirit.SpiritType = Spirit.SpiritTypes.Paper;
                    break;
                case "Ember":
                    tempSpirit.SpiritType = Spirit.SpiritTypes.Ember;
                    break;
                case "Bone":
                    tempSpirit.SpiritType = Spirit.SpiritTypes.Bone;
                    break;
            }

            return tempSpirit;
        }

        return null;
    }
    
    /// <summary>
    /// Method to load all the Spirits from a given file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <returns>A List of Spirits from the file</returns>
    public List<Spirit> LoadSpirits(string fileName) { return LoadMultiples(fileName, LoadSpirit); }

    /// <summary>
    /// Extracts an Adventurer object from the Adventurer file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted Adventurer</returns>
    public Adventurer LoadAdventurer(string fileName, int offset)
    {
        // [CHARACTER]
        // name
        // [NPC]
        // questline name
        // opinions ...
        // [ADVENTURER]
        // TODO: attributes

        string temp = fileName;
        NPC tempNPC = LoadNPC(temp, offset);

        // Making sure that the file exists and that a Character starts at this offset
        if (tempNPC != null)
        {
            Adventurer tempAdventurer = new Adventurer(tempNPC.Name);
            tempAdventurer.Questline = tempNPC.Questline;
            tempAdventurer.Opinions = tempNPC.Opinions;

            return tempAdventurer;
        }

        return null;
    }
    
    /// <summary>
    /// Method to load all the Adventurers from a given file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <returns>A List of Adventurers from the file</returns>
    public List<Adventurer> LoadAdventurers(string fileName) { return LoadMultiples(fileName, LoadAdventurer); }

    /// <summary>
    /// Extracts a Player object from the Player file
    /// (returns null if the file doesn't exist)
    /// </summary>
    /// <param name="fileName">Local file to read data from</param>
    /// <param name="offset">Starting line number in the file</param>
    /// <returns>Extracted Player</returns>
    public Player LoadPlayer(string fileName)
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

        string temp = fileName;
        int offset = 0;
        Character tempCharacter = LoadCharacter(temp, offset);

        // Making sure that the file exists and that a Character starts at this offset
        if (tempCharacter != null)
        {
            string[] lines = LoadLines(temp);

            Player tempPlayer = new Player(tempCharacter.Name);

            while (!lines[++offset].Trim().Equals("[PLAYER]")) { } // Skipping until the Player basic attributes
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    tempPlayer.SpiritClass = Spirit.SpiritClasses.None;
                    break;
                case "Minor":
                    tempPlayer.SpiritClass = Spirit.SpiritClasses.Minor;
                    break;
                case "Median":
                    tempPlayer.SpiritClass = Spirit.SpiritClasses.Median;
                    break;
                case "Major":
                    tempPlayer.SpiritClass = Spirit.SpiritClasses.Major;
                    break;
                case "Magnanimous":
                    tempPlayer.SpiritClass = Spirit.SpiritClasses.Magnanimous;
                    break;
            }
            
            switch (lines[++offset].Trim())
            {
                case "None":
                    tempPlayer.SpiritType = Spirit.SpiritTypes.None;
                    break;
                case "Leaf":
                    tempPlayer.SpiritType = Spirit.SpiritTypes.Leaf;
                    break;
                case "Liquor":
                    tempPlayer.SpiritType = Spirit.SpiritTypes.Liquor;
                    break;
                case "Paper":
                    tempPlayer.SpiritType = Spirit.SpiritTypes.Paper;
                    break;
                case "Ember":
                    tempPlayer.SpiritType = Spirit.SpiritTypes.Ember;
                    break;
                case "Bone":
                    tempPlayer.SpiritType = Spirit.SpiritTypes.Bone;
                    break;
            }
            
            string[] playerPositionSplit = lines[++offset].Trim().Split(' ');
            tempPlayer.SetPlayerPosition(new Vector3(float.Parse(playerPositionSplit[0]), float.Parse(playerPositionSplit[1]), float.Parse(playerPositionSplit[2])));
            string[] playerRotationSplit = lines[++offset].Trim().Split(' ');
            tempPlayer.SetPlayerRotation(new Vector3(float.Parse(playerRotationSplit[0]), float.Parse(playerRotationSplit[1]), float.Parse(playerRotationSplit[2])));
            
            string[] cameraPositionSplit = lines[++offset].Trim().Split(' ');
            tempPlayer.SetCameraRotation(new Vector3(float.Parse(cameraPositionSplit[0]), float.Parse(cameraPositionSplit[1]), float.Parse(cameraPositionSplit[2])));
            string[] cameraRotationSplit = lines[++offset].Trim().Split(' ');
            tempPlayer.SetCameraRotation(new Vector3(float.Parse(cameraRotationSplit[0]), float.Parse(cameraRotationSplit[1]), float.Parse(cameraRotationSplit[2])));

            // Skipping over anything that's not a Player questline (complex attributes)
            while (offset + 1 < lines.Length && !lines[++offset].Trim().Equals("[CHARACTER]"))
            {
                tempPlayer.Questlines.Add(lines[offset].Trim());
            }

            return tempPlayer;
        }

        return null;
    }
}
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character subclass for the player
/// </summary>
public class Player : Character
{
    // TODO: settings
    
    // TODO: inventory
    
    // TODO: cosmetic items
    
    /// <summary>
    /// The player's class
    /// </summary>
    Spirit.SpiritClasses spiritClass;
    
    public Spirit.SpiritClasses SpiritClass
    {
        get => spiritClass;
        set => spiritClass = value;
    }
    
    /// <summary>
    /// The player's type
    /// </summary>
    Spirit.SpiritTypes spiritType;

    public Spirit.SpiritTypes SpiritType
    {
        get => spiritType;
        set => spiritType = value;
    }

    /// <summary>
    /// Last saved player position in the environment
    /// </summary>
    Vector3 playerPosition;
    
    public Vector3 PlayerPosition
    {
        get => playerPosition;
    }
    public void SetPlayerPosition(Vector3 value) { playerPosition = value; }
    
    /// <summary>
    /// Last saved player rotation in the environment
    /// </summary>
    Vector3 playerRotation;
    
    public Vector3 PlayerRotation
    {
        get => playerRotation;
    }
    public void SetPlayerRotation(Vector3 value) { playerRotation = value; }
    
    /// <summary>
    /// Last saved camera position in the environment
    /// </summary>
    Vector3 cameraPosition;
    
    public Vector3 CameraPosition
    {
        get => cameraPosition;
    }
    public void SetCameraPosition(Vector3 value) { cameraPosition = value; }
    
    /// <summary>
    /// Last saved camera rotation in the environment
    /// </summary>
    Vector3 cameraRotation;
    
    public Vector3 CameraRotation
    {
        get => cameraRotation;
    }
    public void SetCameraRotation(Vector3 value) { cameraRotation = value; }
    
    /// <summary>
    /// The player's added questlines
    /// </summary>
    List<string> questlines;
    
    public List<string> Questlines
    {
        get => questlines;
        set => questlines = value;
    }
    
    /// <summary>
    /// Default constructor
    /// </summary>
    public Player() : base()
    {
        spiritClass = Spirit.SpiritClasses.None;
        spiritType = Spirit.SpiritTypes.None;
        
        playerPosition = Vector3.zero;
        playerRotation = Vector3.zero;
        
        cameraPosition = Vector3.zero;
        cameraRotation = Vector3.zero;
        
        questlines = new List<string>();
    }
    
    /// <summary>
    /// Name parameterized constructor
    /// </summary>
    /// <param name="name">Character string name</param>
    public Player(string name) : base(name)
    {
        spiritClass = Spirit.SpiritClasses.None;
        spiritType = Spirit.SpiritTypes.None;
        
        playerPosition = Vector3.zero;
        playerRotation = Vector3.zero;
        
        cameraPosition = Vector3.zero;
        cameraRotation = Vector3.zero;
        
        questlines = new List<string>();
    }
    
    /// <summary>
    /// Full parameterized constructor
    /// </summary>
    /// <param name="name">Character string name</param>
    /// <param name="spiritClass">Spirit class</param>
    /// <param name="spiritType">Spirit type</param>
    public Player(string name, Spirit.SpiritClasses spiritClass, Spirit.SpiritTypes spiritType) : base(name)
    {
        this.spiritClass = spiritClass;
        this.spiritType = spiritType;
        
        playerPosition = Vector3.zero;
        playerRotation = Vector3.zero;
        
        cameraPosition = Vector3.zero;
        cameraRotation = Vector3.zero;
        
        questlines = new List<string>();
    }
    
    public override string BasicAttributes() { return base.BasicAttributes() + "\n[PLAYER]\n" + spiritClass + "\n" + spiritType; } // Appending the basic Player attributes
    public override string ComplexAttributes()
    {
        string temp = base.ComplexAttributes(); // Appending the complex Player attributes
        
        temp += "\n" + playerPosition.x + " " + playerPosition.y + " " + playerPosition.z;
        temp += "\n" + playerRotation.x + " " + playerRotation.y + " " + playerRotation.z;
        
        temp += "\n" + cameraPosition.x + " " + cameraPosition.y + " " + cameraPosition.z;
        temp += "\n" + cameraRotation.x + " " + cameraRotation.y + " " + cameraRotation.z;
        
        if (questlines.Count != 0)
        {
            foreach (string i in questlines)
            {
                temp += "\n" + i;
            }
        }
        
        return temp;
    }
}
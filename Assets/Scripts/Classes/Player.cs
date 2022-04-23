using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // TODO: settings
    
    // TODO: inventory
    
    // TODO: cosmetic items
    
    Spirit.SpiritClasses spiritClass;
    
    public Spirit.SpiritClasses SpiritClass
    {
        get => spiritClass;
        set => spiritClass = value;
    }
    
    Spirit.SpiritTypes spiritType;

    public Spirit.SpiritTypes SpiritType
    {
        get => spiritType;
        set => spiritType = value;
    }

    Vector3 playerPosition;
    public Vector3 PlayerPosition
    {
        get => playerPosition;
    }
    public void SetPlayerPosition(Vector3 value) { playerPosition = value; }
    
    Vector3 playerRotation;
    public Vector3 PlayerRotation
    {
        get => playerRotation;
    }
    public void SetPlayerRotation(Vector3 value) { playerRotation = value; }
    
    Vector3 cameraPosition;
    public Vector3 CameraPosition
    {
        get => cameraPosition;
    }
    public void SetCameraPosition(Vector3 value) { cameraPosition = value; }
    
    Vector3 cameraRotation;
    public Vector3 CameraRotation
    {
        get => cameraRotation;
    }
    public void SetCameraRotation(Vector3 value) { cameraRotation = value; }
    
    List<string> questlines;
    
    public List<string> Questlines
    {
        get => questlines;
        set => questlines = value;
    }
    
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
    
    public override string BasicAttributes() { return base.BasicAttributes() + "\n[PLAYER]\n" + spiritClass + "\n" + spiritType; }
    public override string ComplexAttributes()
    {
        string temp = "";
        
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
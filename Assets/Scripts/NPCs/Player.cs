using System.Collections.Generic;
using UnityEngine;

public class Player : Spirit
{
    Dictionary<string, int> questlines;
    
    public Dictionary<string, int> Questlines
    {
        get => questlines;
        set => questlines = value;
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
    
    public Player() : base()
    {
        questlines = new Dictionary<string, int>();
        
        playerPosition = Vector3.zero;
        playerRotation = Vector3.zero;
        
        cameraPosition = Vector3.zero;
        cameraRotation = Vector3.zero;
    }
    
    public Player(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name, spiritClass, spiritType)
    {
        questlines = new Dictionary<string, int>();
        
        playerPosition = Vector3.zero;
        playerRotation = Vector3.zero;
        
        cameraPosition = Vector3.zero;
        cameraRotation = Vector3.zero;
    }
    
    public override string StringValue()
    {
        string temp = base.StringValue();

        temp += "\n" + playerPosition.x + " " + playerPosition.y + " " + playerPosition.z;
        temp += "\n" + playerRotation.x + " " + playerRotation.y + " " + playerRotation.z;
        
        temp += "\n" + cameraPosition.x + " " + cameraPosition.y + " " + cameraPosition.z;
        temp += "\n" + cameraRotation.x + " " + cameraRotation.y + " " + cameraRotation.z;
        
        if (questlines.Count != 0)
        {
            foreach (KeyValuePair<string, int> i in questlines)
            {
                temp += "\n" + i.Key + " " + i.Value;
            }
        }
        
        return temp;
    }
}
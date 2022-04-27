using System.Collections.Generic;

/// <summary>
/// Branching questline to be completed in the game
/// </summary>
public class Questline
{
    /// <summary>
    /// Name of the Questline
    /// </summary>
    string name;

    public string Name
    {
        get => name;
        set => name = value;
    }
    
    /// <summary>
    /// General Questline genres
    /// </summary>
    public enum QuestlineTypes { None }
    
    /// <summary>
    /// This Questline's type
    /// </summary>
    QuestlineTypes questlineType;
    
    public QuestlineTypes QuestlineType
    {
        get => questlineType;
        set => questlineType = value;
    }
    
    /// <summary>
    /// general Questline completion amount
    /// </summary>
    public enum QuestlineStates { Unstarted, Started, Finished }
    
    /// <summary>
    /// This Questline's state
    /// </summary>
    QuestlineStates questlineState;
    
    public QuestlineStates QuestlineState
    {
        get => questlineState;
        set => questlineState = value;
    }
    
    // TODO: requirements
    
    // TODO: rewards

    /// <summary>
    /// List of all the QuestlineNodes that comprise this Questline
    /// (value: QuestlineNode's name)
    /// </summary>
    List<string> nodes;

    public List<string> Nodes
    {
        get => nodes;
        set => nodes = value;
    }

    /// <summary>
    /// Name of the QuestlineNode that this Questline is currently at
    /// </summary>
    string currentNode;

    public string CurrentNode
    {
        get => currentNode;
        set => currentNode = value;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Questline()
    {
        name = "None";
        questlineType = QuestlineTypes.None;
        questlineState = QuestlineStates.Unstarted;
        nodes = new List<string>();
        currentNode = "None";
    }
    
    /// <summary>
    /// Name parameterized constructor
    /// </summary>
    /// <param name="name">Questline string name</param>
    public Questline(string name)
    {
        this.name = name;
        questlineType = QuestlineTypes.None;
        questlineState = QuestlineStates.Unstarted;
        nodes = new List<string>();
        currentNode = "None";
    }
    
    /// <summary>
    /// Full parameterized constructor
    /// </summary>
    /// <param name="name">Questline string name</param>
    /// <param name="questlineType">Questline type</param>
    /// <param name="questlineState">Questline state</param>
    public Questline(string name, QuestlineTypes questlineType, QuestlineStates questlineState)
    {
        this.name = name;
        this.questlineType = questlineType;
        this.questlineState = questlineState;
        nodes = new List<string>();
        currentNode = "None";
    }

    /// <summary>
    /// All class attributes
    /// </summary>
    /// <returns>Attribute lines</returns>
    public string StringValue()
    {
        string temp = "[QUESTLINE]\n" + name + "\n" + questlineType + "\n" + questlineState;

        foreach (string i in nodes)
        {
            temp += "\n" + i;
            
            if (i.Equals(currentNode))
            {
                temp += " (current)";
            }
        }
        
        return temp;
    }
}
using System.Collections.Generic;

public class Questline
{
    string name;

    public string Name
    {
        get => name;
        set => name = value;
    }
    
    public enum QuestlineTypes { None }
    QuestlineTypes questlineType;
    
    public QuestlineTypes QuestlineType
    {
        get => questlineType;
        set => questlineType = value;
    }
    
    public enum QuestlineStates { Unstarted, Started, Finished }
    QuestlineStates questlineState;
    
    public QuestlineStates QuestlineState
    {
        get => questlineState;
        set => questlineState = value;
    }
    
    // TODO: requirements
    
    // TODO: rewards

    List<string> nodes;

    public List<string> Nodes
    {
        get => nodes;
        set => nodes = value;
    }

    string currentNode;

    public string CurrentNode
    {
        get => currentNode;
        set => currentNode = value;
    }

    public Questline()
    {
        name = "None";
        questlineType = QuestlineTypes.None;
        questlineState = QuestlineStates.Unstarted;
        nodes = new List<string>();
        currentNode = "None";
    }
    
    public Questline(string name)
    {
        this.name = name;
        questlineType = QuestlineTypes.None;
        questlineState = QuestlineStates.Unstarted;
        nodes = new List<string>();
        currentNode = "None";
    }
    
    public Questline(string name, QuestlineTypes questlineType, QuestlineStates questlineState)
    {
        this.name = name;
        this.questlineType = questlineType;
        this.questlineState = questlineState;
        nodes = new List<string>();
        currentNode = "None";
    }

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
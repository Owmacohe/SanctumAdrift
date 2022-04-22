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

    List<QuestlineNode> nodes;

    public List<QuestlineNode> Nodes
    {
        get => nodes;
        set => nodes = value;
    }

    QuestlineNode currentNode;

    public QuestlineNode CurrentNode
    {
        get => currentNode;
        set => currentNode = value;
    }
    
    // TODO: requirements
    
    // TODO: rewards

    public Questline()
    {
        name = "";
        questlineType = QuestlineTypes.None;
        questlineState = QuestlineStates.Unstarted;
        nodes = new List<QuestlineNode>();
        currentNode = null;
    }
    
    public Questline(string name)
    {
        this.name = name;
        questlineType = QuestlineTypes.None;
        questlineState = QuestlineStates.Unstarted;
        nodes = new List<QuestlineNode>();
        currentNode = null;
    }
    
    public Questline(string name, QuestlineTypes questlineType, QuestlineStates questlineState)
    {
        this.name = name;
        this.questlineType = questlineType;
        this.questlineState = questlineState;
        nodes = new List<QuestlineNode>();
        currentNode = null;
    }

    public string StringValue()
    {
        string temp = name + "\n" + questlineType + "\n" + questlineState;

        for (int i = 0; i < nodes.Count; i++)
        {
            temp += "\nNode" + i;

            if (nodes[i].Equals(currentNode))
            {
                temp += " (current)";
            }
        }
        
        return temp;
    }
}
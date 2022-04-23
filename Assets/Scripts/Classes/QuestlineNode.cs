public class QuestlineNode
{
    string name;

    public string Name
    {
        get => name;
        set => name = value;
    }
    
    string questline;

    public string Questline
    {
        get => questline;
        set => questline = value;
    }
    
    // TODO: requirements

    string previous;

    public string Previous
    {
        get => previous;
        set => previous = value;
    }
    
    string next;

    public string Next
    {
        get => next;
        set => next = value;
    }

    public QuestlineNode()
    {
        name = "";
        questline = null;
        previous = null;
        next = null;
    }
    
    public QuestlineNode(string name)
    {
        this.name = name;
        questline = null;
        previous = null;
        next = null;
    }
    
    public QuestlineNode(string name, string questline)
    {
        this.name = name;
        this.questline = questline;
        previous = null;
        next = null;
    }
    
    public QuestlineNode(string name, string questline, string previous, string next)
    {
        this.name = name;
        this.questline = questline;
        this.previous = previous;
        this.next = next;
    }

    public string StringValue() { return "[NODE]\n" + name + "\n" + questline + "\n" + previous + "\n" + next; }
}
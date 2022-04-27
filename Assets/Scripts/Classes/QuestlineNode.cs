/// <summary>
/// Single node of a branching Questline
/// </summary>
public class QuestlineNode
{
    /// <summary>
    /// Name of the QuestlineNode
    /// </summary>
    string name;

    public string Name
    {
        get => name;
        set => name = value;
    }
    
    /// <summary>
    /// Name of the QuestlineNode's Questline
    /// </summary>
    string questline;

    public string Questline
    {
        get => questline;
        set => questline = value;
    }
    
    // TODO: requirements

    /// <summary>
    /// Name of the QuestlineNode that leads to this one
    /// </summary>
    string previous;

    public string Previous
    {
        get => previous;
        set => previous = value;
    }
    
    /// <summary>
    /// Name of the QuestlineNode that follows to this one
    /// </summary>
    string next;

    public string Next
    {
        get => next;
        set => next = value;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public QuestlineNode()
    {
        name = "None";
        questline = null;
        previous = null;
        next = null;
    }
    
    /// <summary>
    /// QuestlineNode name parameterized constructor
    /// </summary>
    /// <param name="name">QuestlineNode string name</param>
    public QuestlineNode(string name)
    {
        this.name = name;
        questline = null;
        previous = null;
        next = null;
    }
    
    /// <summary>
    /// QuestlineNode and Questline names parameterized constructor
    /// </summary>
    /// <param name="name">QuestlineNode string name</param>
    /// <param name="questline">Questline string name</param>
    public QuestlineNode(string name, string questline)
    {
        this.name = name;
        this.questline = questline;
        previous = null;
        next = null;
    }
    
    /// <summary>
    /// Full parameterized constructor
    /// </summary>
    /// <param name="name">QuestlineNode string name</param>
    /// <param name="questline">Questline string name</param>
    /// <param name="previous">Previous QuestlineNode string name</param>
    /// <param name="next">Next QuestlineNode string name</param>
    public QuestlineNode(string name, string questline, string previous, string next)
    {
        this.name = name;
        this.questline = questline;
        this.previous = previous;
        this.next = next;
    }

    /// <summary>
    /// All class attributes
    /// </summary>
    /// <returns>Attribute lines</returns>
    public string StringValue() { return "[NODE]\n" + name + "\n" + questline + "\n" + previous + "\n" + next; }
}
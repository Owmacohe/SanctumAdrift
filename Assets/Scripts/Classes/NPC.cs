using System.Collections.Generic;

/// <summary>
/// Character subclass for all Non-Player Characters
/// </summary>
public class NPC : Character
{
    /// <summary>
    /// Name of the questline assigned to this NPC
    /// </summary>
    string questline;

    public string Questline
    {
        get => questline;
        set => questline = value;
    }
    
    /// <summary>
    /// This NPC's opinions of other Characters
    /// (key: Character's name, value: opinion value)
    /// </summary>
    Dictionary<string, int> opinions;

    public Dictionary<string, int> Opinions
    {
        get => opinions;
        set => opinions = value;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public NPC() : base()
    {
        questline = "None";
        opinions = new Dictionary<string, int>();
    }

    /// <summary>
    /// Parameterized constructor
    /// </summary>
    /// <param name="name">Character string name</param>
    public NPC(string name) : base(name)
    {
        questline = "None";
        opinions = new Dictionary<string, int>();
    }
    
    public override string BasicAttributes() { return base.BasicAttributes() + "\n[NPC]\n" + questline; } // Appending the basic NPC attributes
    public override string ComplexAttributes()
    {
        string temp = base.ComplexAttributes(); // Appending the complex NPC attributes
        
        // Adding each opinion pair as a new line
        foreach (KeyValuePair<string, int> i in opinions)
        {
            temp += "\n" + i.Key + " " + i.Value;
        }

        return temp;
    }
}
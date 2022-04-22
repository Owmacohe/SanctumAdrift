using System.Collections.Generic;

public class NPC : Character
{
    Dictionary<Character, int> opinions;

    public Dictionary<Character, int> Opinions
    {
        get => opinions;
        set => opinions = value;
    }

    Questline questline;

    public Questline Questline
    {
        get => questline;
        set => questline = value;
    }
    
    public NPC() : base() { opinions = new Dictionary<Character, int>(); }
    public NPC(string name) : base(name) { opinions = new Dictionary<Character, int>(); }

    public override string BasicAttributes() { return "---\n" + base.BasicAttributes() + "\n" + questline.StringValue(); }
    public override string ComplexAttributes()
    {
        string temp = "\n===\n";
        
        if (opinions.Count != 0)
        {
            foreach (KeyValuePair<Character, int> i in opinions)
            {
                temp += "\n" + i.Key + " " + i.Value;
            }
        }

        return temp;
    }
}
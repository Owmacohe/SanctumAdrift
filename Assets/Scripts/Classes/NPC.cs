using System.Collections.Generic;

public class NPC : Character
{
    Questline questline;

    public Questline Questline
    {
        get => questline;
        set => questline = value;
    }
    
    Dictionary<string, int> opinions;

    public Dictionary<string, int> Opinions
    {
        get => opinions;
        set => opinions = value;
    }

    public NPC() : base()
    {
        questline = null;
        opinions = new Dictionary<string, int>();
    }

    public NPC(string name) : base(name)
    {
        questline = null;
        opinions = new Dictionary<string, int>();
    }

    public override string BasicAttributes() { return base.BasicAttributes() + "\n[NPC]\n" + questline.Name; }
    public override string ComplexAttributes()
    {
        string temp = "";
        
        if (opinions.Count != 0)
        {
            foreach (KeyValuePair<string, int> i in opinions)
            {
                temp += "\n" + i.Key + " " + i.Value;
            }
        }

        return temp;
    }
}
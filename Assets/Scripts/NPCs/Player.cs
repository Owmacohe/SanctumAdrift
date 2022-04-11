using System.Collections.Generic;

public class Player : Spirit
{
    Dictionary<Spirit, int> questlines;

    public Dictionary<Spirit, int> Questlines
    {
        get { return questlines; }
        set { questlines = value; }
    }
    
    public Player() : base()
    {
        questlines = new Dictionary<Spirit, int>();
    }
    
    public Player(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name, spiritClass, spiritType)
    {
        questlines = new Dictionary<Spirit, int>();
    }
    
    public override string StringValue()
    {
        string temp = base.StringValue();
        
        if (questlines.Count != 0)
        {
            foreach (KeyValuePair<Spirit, int> i in questlines)
            {
                temp += "\n" + i.Key.Name + " " + i.Value;
            }
        }
        
        return temp;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class NPC : Spirit
{
    Dictionary<Spirit, int> opinions;

    public Dictionary<Spirit, int> Opinions
    {
        get { return opinions; }
        set { opinions = value; }
    }
    
    public NPC() : base()
    {
        opinions = new Dictionary<Spirit, int>();
    }
    
    public NPC(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name, spiritClass, spiritType)
    {
        opinions = new Dictionary<Spirit, int>();
    }

    public override string StringValue()
    {
        string temp = "---\n" + base.StringValue();

        if (opinions.Count != 0)
        {
            temp += "\n===";
            
            foreach (KeyValuePair<Spirit, int> i in opinions)
            {
                temp += "\n" + i.Key.Name + " " + i.Value;
            }
        }

        return temp;
    }
}
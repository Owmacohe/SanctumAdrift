using System.Collections.Generic;
using UnityEngine;

public class NPC : Spirit
{
    Dictionary<string, int> opinions;

    public Dictionary<string, int> Opinions
    {
        get => opinions;
        set => opinions = value;
    }
    
    public NPC() : base()
    {
        opinions = new Dictionary<string, int>();
    }
    
    public NPC(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name, spiritClass, spiritType)
    {
        opinions = new Dictionary<string, int>();
    }

    public override string StringValue()
    {
        string temp = "---\n" + base.StringValue();

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
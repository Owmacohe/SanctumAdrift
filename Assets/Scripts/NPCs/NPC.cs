using System.Collections.Generic;

public class NPC : Spirit
{
    List<KeyValuePair<Spirit, int>> opinions;
    
    public List<KeyValuePair<Spirit, int>> Opinions
    {
        get { return opinions; }
        set { opinions = value; }
    }
    
    public NPC() : base()
    {
        opinions = new List<KeyValuePair<Spirit, int>>();
    }
    
    public NPC(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name, spiritClass, spiritType)
    {
        opinions = new List<KeyValuePair<Spirit, int>>();
    }
}
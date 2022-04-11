using System.Collections.Generic;

public class Player : Spirit
{
    List<KeyValuePair<Spirit, int>> questlines;

    public List<KeyValuePair<Spirit, int>> Questlines
    {
        get { return questlines; }
        set { questlines = value; }
    }
    
    public Player() : base()
    {
        questlines = new List<KeyValuePair<Spirit, int>>();
    }
    
    public Player(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name, spiritClass, spiritType)
    {
        questlines = new List<KeyValuePair<Spirit, int>>();
    }
}
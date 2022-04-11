using UnityEngine;

public class Spirit
{
    string name;
    
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public enum SpiritClasses { None, Minor, Median, Major, Magnanimous };
    SpiritClasses spiritClass;
    
    public SpiritClasses SpiritClass
    {
        get { return spiritClass; }
        set { spiritClass = value; }
    }
    
    public enum SpiritTypes { None, Leaf, Liquor, Paper, Ember, Bone };
    SpiritTypes spiritType;

    public SpiritTypes SpiritType
    {
        get { return spiritType; }
        set { spiritType = value; }
    }

    public Spirit()
    {
        name = "";
        spiritClass = SpiritClasses.None;
        spiritType = SpiritTypes.None;
    }
    
    public Spirit(string name, SpiritClasses spiritClass, SpiritTypes spiritType)
    {
        this.name = name;
        this.spiritClass = spiritClass;
        this.spiritType = spiritType;
    }

    public virtual string StringValue()
    {
        return name + "\n" + spiritClass + "\n" + spiritType;
    }
}
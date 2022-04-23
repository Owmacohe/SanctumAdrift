public class Spirit : NPC
{
    public enum SpiritClasses { None, Minor, Median, Major, Magnanimous };
    SpiritClasses spiritClass;
    
    public SpiritClasses SpiritClass
    {
        get => spiritClass;
        set => spiritClass = value;
    }
    
    public enum SpiritTypes { None, Leaf, Liquor, Paper, Ember, Bone };
    SpiritTypes spiritType;

    public SpiritTypes SpiritType
    {
        get => spiritType;
        set => spiritType = value;
    }

    public Spirit() : base()
    {
        spiritClass = SpiritClasses.None;
        spiritType = SpiritTypes.None;
    }

    public Spirit(string name) : base(name)
    {
        spiritClass = SpiritClasses.None;
        spiritType = SpiritTypes.None;
    }
    
    public Spirit(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name)
    {
        this.spiritClass = spiritClass;
        this.spiritType = spiritType;
    }

    public override string ComplexAttributes() { return base.ComplexAttributes() + "\n[SPIRIT]\n" + spiritClass + "\n" + spiritType; }
}
/// <summary>
/// Spirit inhabitant NPC subclass
/// </summary>
public class Spirit : NPC
{
    /// <summary>
    /// Spirit hierarchy
    /// </summary>
    public enum SpiritClasses { None, Minor, Median, Major, Magnanimous }
    
    /// <summary>
    /// This Spirit's class
    /// </summary>
    SpiritClasses spiritClass;
    
    public SpiritClasses SpiritClass
    {
        get => spiritClass;
        set => spiritClass = value;
    }
    
    /// <summary>
    /// Spirit genres/flavours/races
    /// </summary>
    public enum SpiritTypes { None, Leaf, Liquor, Paper, Ember, Bone }
    
    /// <summary>
    /// This Spirit's type
    /// </summary>
    SpiritTypes spiritType;

    public SpiritTypes SpiritType
    {
        get => spiritType;
        set => spiritType = value;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Spirit() : base()
    {
        spiritClass = SpiritClasses.None;
        spiritType = SpiritTypes.None;
    }

    /// <summary>
    /// Name parameterized constructor
    /// </summary>
    /// <param name="name">Character string name</param>
    public Spirit(string name) : base(name)
    {
        spiritClass = SpiritClasses.None;
        spiritType = SpiritTypes.None;
    }
    
    /// <summary>
    /// Full parameterized constructor
    /// </summary>
    /// <param name="name">Character string name</param>
    /// <param name="spiritClass">Spirit class</param>
    /// <param name="spiritType">Spirit type</param>
    public Spirit(string name, SpiritClasses spiritClass, SpiritTypes spiritType) : base(name)
    {
        this.spiritClass = spiritClass;
        this.spiritType = spiritType;
    }

    public override string ComplexAttributes() { return base.ComplexAttributes() + "\n[SPIRIT]\n" + spiritClass + "\n" + spiritType; } // Appending the complex Spirit attributes
}
/// <summary>
/// Invading Adventurer NPC subclass
/// </summary>
public class Adventurer : NPC
{
    // TODO: attributes
    
    /// <summary>
    /// Default constructor
    /// </summary>
    public Adventurer() : base() { }
    
    /// <summary>
    /// Parameterized constructor
    /// </summary>
    /// <param name="name">Character string name</param>
    public Adventurer(string name) : base(name) { }
    
    public override string BasicAttributes() { return base.BasicAttributes() + "\n[ADVENTURER]"; } // Appending the basic Adventurer attributes
}
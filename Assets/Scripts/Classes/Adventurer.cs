public class Adventurer : NPC
{
    // TODO: attributes
    
    public Adventurer() : base() { }
    public Adventurer(string name) : base(name) { }
    
    public override string ComplexAttributes() { return base.ComplexAttributes() + "\n[ADVENTURER]"; }
}
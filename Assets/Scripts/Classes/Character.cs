public abstract class Character
{
    string name;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public Character() { name = ""; }
    public Character(string name) { this.name = name; }
    
    public virtual string BasicAttributes() { return name; }
    public virtual string ComplexAttributes() { return ""; }
    
    public string StringValue() { return BasicAttributes() + ComplexAttributes(); }
}
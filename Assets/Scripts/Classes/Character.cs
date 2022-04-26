public class Character
{
    string name;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public Character() { name = "None"; }
    public Character(string name) { this.name = name; }
    
    public virtual string BasicAttributes() { return "[CHARACTER]\n" + name; }
    public virtual string ComplexAttributes() { return ""; }
    
    public string StringValue() { return BasicAttributes() + ComplexAttributes(); }
}
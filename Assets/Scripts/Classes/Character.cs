/// <summary>
/// Base class for all entities in the game
/// </summary>
public class Character
{
    /// <summary>
    /// Name of the character
    /// </summary>
    string name;

    public string Name
    {
        get => name;
        set => name = value;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Character() { name = "None"; }
    
    /// <summary>
    /// Parameterized constructor
    /// </summary>
    /// <param name="name">Character string name</param>
    public Character(string name) { this.name = name; }
    
    /// <summary>
    /// Fixed-length class attributes
    /// </summary>
    /// <returns>Basic attributes lines</returns>
    public virtual string BasicAttributes() { return "[CHARACTER]\n" + name; }
    
    /// <summary>
    /// Changing-length class attributes
    /// </summary>
    /// <returns>Complex attributes lines</returns>
    public virtual string ComplexAttributes() { return ""; }
    
    /// <summary>
    /// All class attibutes
    /// </summary>
    /// <returns>Basic and complex attributes lines</returns>
    public string StringValue() { return BasicAttributes() + ComplexAttributes(); }
}
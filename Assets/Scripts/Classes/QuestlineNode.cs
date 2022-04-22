public class QuestlineNode
{
    Questline questline;

    public Questline Questline
    {
        get => questline;
        set => questline = value;
    }
    
    // TODO: requirements

    QuestlineNode previous;

    public QuestlineNode Previous
    {
        get => previous;
        set => previous = value;
    }
    
    QuestlineNode next;

    public QuestlineNode Next
    {
        get => next;
        set => next = value;
    }

    public QuestlineNode()
    {
        questline = null;
        previous = null;
        next = null;
    }
    
    public QuestlineNode(Questline questline)
    {
        this.questline = questline;
        previous = null;
        next = null;
    }
    
    public QuestlineNode(Questline questline, QuestlineNode previous, QuestlineNode next)
    {
        this.questline = questline;
        this.previous = previous;
        this.next = next;
    }
}
namespace ASLET.Objects.Conditions;

public abstract class Condition
{
    private HourNode Node { get; set; }

    protected Condition(HourNode node)
    {
        Node = node;
    }

    public abstract bool Complete();

    public abstract bool UnComplete();
}
namespace ASLET.Services.Objects.Conditions;

public abstract class Condition
{
    private HourNode Node { get; }

    protected Condition(HourNode node)
    {
        Node = node;
    }

    public abstract bool Complete();

    public abstract bool UnComplete();
}
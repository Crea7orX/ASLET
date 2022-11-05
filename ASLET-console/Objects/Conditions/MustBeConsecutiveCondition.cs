namespace ASLET.Objects.Conditions;

public class MustBeConsecutiveCondition : Condition
{
    private SubjectExample _subjectExample;
    public const int CODE = 1;

    public MustBeConsecutiveCondition(HourNode node, SubjectExample subjectExample) : base(node)
    {
        _subjectExample = subjectExample;
    }

    public override bool Complete()
    {
        return true;
    }

    public override bool UnComplete()
    {
        return true;
    }
}
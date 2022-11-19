namespace ASLET.Services.Objects;

public class SubjectExample
{
    public static readonly SubjectExample Empty = new SubjectExample();
    public string Subject { get; set; }
    public string Teacher { get; set; }

    public SubjectExample(string subject = "неизвестно", string teacher = "неизвестно")
    {
        Subject = subject;
        Teacher = teacher;
    }

    public override bool Equals(object? obj)
    {
        return ((SubjectExample)obj).Teacher.Equals(Teacher) && ((SubjectExample)obj).Subject.Equals(Subject);
    }

    public override int GetHashCode()
    {
        int hash = 7;
        hash = 29 * hash + Subject.GetHashCode();
        hash = 29 * hash + Teacher.GetHashCode();
        return hash;
    }
}
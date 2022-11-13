namespace ASLET.Server.Contracts.Models;

public class SubjectExample
{
    public string Subject { get; }
    public string Teacher { get; }

    public SubjectExample(string subject, string teacher)
    {
        Subject = subject;
        Teacher = teacher;
    }
}
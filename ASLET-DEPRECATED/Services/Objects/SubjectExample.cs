using System;

namespace ASLET.Services.Objects;

public class SubjectExample
{
    public static readonly SubjectExample Empty = new SubjectExample();

    public Guid Id { get; }
    public Guid SubjectId { get; set; }
    public Guid TeacherId { get; set; }

    public string Subject { get; set; }
    public string Teacher { get; set; }

    public SubjectExample(Guid subjectId, Guid teacherId, string subject = "неизвестно", string teacher = "неизвестно")
    {
        Id = Guid.NewGuid();
        SubjectId = subjectId;
        TeacherId = teacherId;
        Subject = subject;
        Teacher = teacher;
    }

    public SubjectExample(string subject = "неизвестно", string teacher = "неизвестно")
    {
        Id = Guid.Empty;
        SubjectId = Guid.Empty;
        TeacherId = Guid.Empty;
        Subject = subject;
        Teacher = teacher;
    }

    public override bool Equals(object? obj)
    {
        return (((SubjectExample)obj).SubjectId.Equals(SubjectId) && ((SubjectExample)obj).TeacherId.Equals(TeacherId)) || ((SubjectExample)obj).TeacherId.Equals(TeacherId) || ((SubjectExample)obj).Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        int hash = 7;
        hash = 29 * hash + SubjectId.GetHashCode();
        hash = 29 * hash + TeacherId.GetHashCode();
        return hash;
    }
}
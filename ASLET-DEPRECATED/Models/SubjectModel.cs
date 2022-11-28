using System;

namespace ASLET.Models;

public class SubjectModel
{
    public Guid SubjectId { get; }
    public string? Name { get; }

    public SubjectModel(string name = "")
    {
        SubjectId = Guid.NewGuid();
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is HourModel) return SubjectId == ((HourModel)obj).SubjectId;
        if (obj is SubjectModel) return ((SubjectModel)obj).SubjectId == SubjectId;
        return false;
    }
}
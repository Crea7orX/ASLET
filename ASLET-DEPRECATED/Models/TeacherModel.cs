using System;

namespace ASLET.Models;

public class TeacherModel
{
    public Guid TeacherId { get; }
    public string? Name { get; }

    public TeacherModel(string name = "")
    {
        TeacherId = Guid.NewGuid();
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is HourModel) return TeacherId == ((HourModel)obj).TeacherId;
        if (obj is TeacherModel) return ((TeacherModel)obj).TeacherId == TeacherId;
        return false;
    }
}
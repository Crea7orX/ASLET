using System;

namespace ASLET.Models;

public class ClassModel
{
    public Guid ClassId { get; }
    public byte Grade { get; }
    public char? Letter { get; }

    public ClassModel(byte grade = 0, char letter = ' ')
    {
        ClassId = Guid.NewGuid();
        Grade = grade;
        Letter = letter;
    }

    public override string ToString()
    {
        return Grade.ToString() + Letter;
    }

    public override bool Equals(object? obj)
    {
        if (obj is HourModel) return ClassId == ((HourModel)obj).ClassId;
        if (obj is ClassModel) return ((ClassModel)obj).ClassId == ClassId;
        return false;
    }
}
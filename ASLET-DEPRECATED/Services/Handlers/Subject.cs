using System;

namespace ASLET.Services.Handlers;

public class Subject
{
    public const int EASY = 1;
    public const int MEDIUM = 2;
    public const int HARD = 3;

    public Guid Id { get; }
    
    public string Name { get; }
    public int Hardness { get; }

    public Subject(Guid id, string name, int hardness = EASY)
    {
        Id = id;
        Name = name;
        Hardness = hardness;
    }
}
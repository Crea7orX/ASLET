namespace ASLET.Services.Handlers;

public class Subject
{
    public const int EASY = 1;
    public const int MEDIUM = 2;
    public const int HARD = 3;

    public string Name { get; }
    public int Hardness { get; }

    public Subject(string name, int hardness = EASY)
    {
        Name = name;
        Hardness = hardness;
    }
}
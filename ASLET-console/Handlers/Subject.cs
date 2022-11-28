namespace ASLET.Handlers;

public class Subject
{
    public const int EASY = 1;
    public const int MEDIUM = 2;
    public const int HARD = 3;

    public string Name { get; private set; }
    public int Hardness { get; set; }

    public Subject(string name, int hardness = EASY)
    {
        this.Name = name;
        this.Hardness = hardness;
    }
}
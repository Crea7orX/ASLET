namespace ASLET.Models;

public class SubjectModel
{
    public string? Name { get; }

    public SubjectModel(string name = "")
    {
        Name = name;
    }
}
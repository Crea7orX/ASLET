namespace ASLET.Models;

public class TeacherModel
{
    public string Name { get; }

    public TeacherModel(string name = "")
    {
        Name = name;
    }
}
namespace ASLET.Models;

public class SubjectModel
{
    private static int _nextSubjectId = 0;
    // Initializes course
    public SubjectModel(string name)
    {
        Id = _nextSubjectId++;
        Name = name;
    }

    // Returns course ID
    public int Id { get; set; }

    // Returns course name
    public string Name { get; set; }
}
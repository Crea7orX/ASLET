namespace ASLET.Models;

public class SubjectModel
{
    private static int _nextSubjectId = 0;
    // Initializes course
    public SubjectModel(string name, string uniqueId = "")
    {
        UniqueId = uniqueId;
        Id = _nextSubjectId++;
        Name = name;
    }

    public string UniqueId { get; set; }

    // Returns course ID
    public int Id { get; set; }

    // Returns course name
    public string Name { get; set; }
}
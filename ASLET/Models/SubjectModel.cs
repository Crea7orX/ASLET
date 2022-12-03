namespace ASLET.Models;

public class SubjectModel
{
    private static int _nextSubjectId = -1;
    // Initializes course
    public SubjectModel(string name, bool updateId = false, string uniqueId = "")
    {
        if (updateId) _nextSubjectId++;
        UniqueId = uniqueId;
        Id = _nextSubjectId;
        Name = name;
    }

    public string UniqueId { get; set; }

    // Returns course ID
    public int Id { get; set; }

    // Returns course name
    public string Name { get; set; }
}
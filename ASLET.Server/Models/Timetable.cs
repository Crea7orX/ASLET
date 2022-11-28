using ASLET.Server.Contracts.Models;
using ASLET.Server.Contracts.Timetable;
using ErrorOr;

namespace ASLET.Server.Models;

public class Timetable
{
    public Guid Id { get; }
    public string ClassName { get; }
    public Dictionary<SubjectExample, int> Subjects { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastModified { get; }

    private Timetable(Guid id, string className, Dictionary<SubjectExample, int> subjects, Guid createdBy)
    {
        Id = id;
        ClassName = className;
        Subjects = subjects;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now; // TODO NOT WORKING PROPERLY
        LastModified = DateTime.Now;
    }

    public static ErrorOr<Timetable> Create(string className, Dictionary<SubjectExample, int> subjects,
        Guid createdBy, Guid? id = null)
    {
        // TODO CHECKERS

        return new Timetable(id ?? Guid.NewGuid(), className, subjects, createdBy);
    }

    public static ErrorOr<Timetable> From(CreateTimetableRequest request)
    {
        return Create(request.ClassName, request.Subjects, request.CreatedBy);
    }

    public static ErrorOr<Timetable> From(Guid id, UpsertTimetableRequest request)
    {
        return Create(request.ClassName, request.Subjects, request.CreatedBy, id);
    }
}
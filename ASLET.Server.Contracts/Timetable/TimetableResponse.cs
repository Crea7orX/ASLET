using ASLET.Server.Contracts.Models;

namespace ASLET.Server.Contracts.Timetable;

public record TimetableResponse(Guid Id, string ClassName, Dictionary<SubjectExample, int> Subjects, Guid CreatedBy,
    DateTime CreatedAt, DateTime LastModified);
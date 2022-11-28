using ASLET.Server.Contracts.Models;

namespace ASLET.Server.Contracts.Timetable;

public record UpsertTimetableRequest(string ClassName, Dictionary<SubjectExample, int> Subjects, Guid CreatedBy);
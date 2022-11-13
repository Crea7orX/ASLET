using ASLET.Server.Contracts.Models;

namespace ASLET.Server.Contracts.Timetable;

public record CreateTimetableRequest(string ClassName, Dictionary<SubjectExample, int> Subjects, Guid CreatedBy);
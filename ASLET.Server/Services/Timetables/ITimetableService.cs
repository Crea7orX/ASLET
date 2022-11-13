using ASLET.Server.Models;
using ErrorOr;

namespace ASLET.Server.Services.Timetables;

public interface ITimetableService
{
    ErrorOr<Created> CreateTimetable(Timetable timetable);
    ErrorOr<Timetable> GetTimetable(Guid id);
    ErrorOr<UpsertedTimetable> UpsertTimetable(Timetable timetable);
    ErrorOr<Deleted> DeleteTimetable(Guid id);
}
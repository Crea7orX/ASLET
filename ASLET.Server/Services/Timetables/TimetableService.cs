using ASLET.Server.Models;
using ASLET.Server.ServiceErrors;
using ErrorOr;

namespace ASLET.Server.Services.Timetables;

public class TimetableService : ITimetableService
{
    private static readonly Dictionary<Guid, Timetable> Timetables = new();

    public ErrorOr<Created> CreateTimetable(Timetable timetable)
    {
        Timetables.Add(timetable.Id, timetable);

        return Result.Created;
    }

    public ErrorOr<Timetable> GetTimetable(Guid id)
    {
        if (Timetables.TryGetValue(id, out var timetable))
        {
            return timetable;
        }

        return Errors.Timetable.NotFound;
    }

    public ErrorOr<UpsertedTimetable> UpsertTimetable(Timetable timetable)
    {
        bool isNewlyCreated = !Timetables.ContainsKey(timetable.Id);
        Timetables[timetable.Id] = timetable;

        return new UpsertedTimetable(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteTimetable(Guid id)
    {
        Timetables.Remove(id);

        return Result.Deleted;
    }
}
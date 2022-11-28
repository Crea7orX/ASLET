using ErrorOr;
using ASLET.Server.Contracts.Timetable;
using ASLET.Server.Models;
using ASLET.Server.Services.Timetables;
using Microsoft.AspNetCore.Mvc;

namespace ASLET.Server.Controllers;

public class TimetablesController : ApiController
{
    private readonly ITimetableService _timetableService;

    public TimetablesController(ITimetableService timetableService)
    {
        _timetableService = timetableService;
    }

    [HttpPost]
    public IActionResult CreateTimetable(CreateTimetableRequest request)
    {
        ErrorOr<Timetable> requestToTimetableResult = Timetable.From(request);

        if (requestToTimetableResult.IsError)
        {
            return Problem(requestToTimetableResult.Errors);
        }

        Timetable timetable = requestToTimetableResult.Value;
        ErrorOr<Created> createTimetableResult = _timetableService.CreateTimetable(timetable);

        return createTimetableResult.Match(created => CreatedAtGetTimetable(timetable), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetTimetable(Guid id)
    {
        ErrorOr<Timetable> getTimetableResult = _timetableService.GetTimetable(id);

        return getTimetableResult.Match(timetable => Ok(MapTimetableResponse(timetable)), errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertTimetable(Guid id, UpsertTimetableRequest request)
    {
        ErrorOr<Timetable> requestToTimetableResult = Timetable.From(id, request);

        if (requestToTimetableResult.IsError)
        {
            return Problem(requestToTimetableResult.Errors);
        }

        Timetable timetable = requestToTimetableResult.Value;
        ErrorOr<UpsertedTimetable> upsertTimetableResult = _timetableService.UpsertTimetable(timetable);

        return upsertTimetableResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetTimetable(timetable) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTimetable(Guid id)
    {
        ErrorOr<Deleted> deleteTimetableResult = _timetableService.DeleteTimetable(id);

        return deleteTimetableResult.Match(deleted => NoContent(), errors => Problem(errors));
    }

    private static TimetableResponse MapTimetableResponse(Timetable timetable)
    {
        return new TimetableResponse(timetable.Id, timetable.ClassName, timetable.Subjects, timetable.CreatedBy,
            timetable.CreatedAt, timetable.LastModified);
    }

    private CreatedAtActionResult CreatedAtGetTimetable(Timetable timetable)
    {
        return CreatedAtAction(actionName: nameof(GetTimetable), routeValues: new { id = timetable.Id },
            value: MapTimetableResponse(timetable));
    }
}
using ErrorOr;

namespace ASLET.Server.ServiceErrors;

public static class Errors
{
    public static class Timetable
    {
        public static Error NotFound => Error.NotFound(
            code: "Timetable.NotFound",
            description: "Timetable not found"
        );
    }
}
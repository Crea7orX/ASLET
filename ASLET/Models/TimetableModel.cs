namespace ASLET.Models;

public class TimetableModel
{
    public string Hour { get; }
    public string Monday { get; }
    public string Tuesday { get; }
    public string Wednesday { get; }
    public string Thursday { get; }
    public string Friday { get; }

    public TimetableModel(string hour, string monday, string tuesday, string wednesday, string thursday, string friday)
    {
        Hour = hour;
        Monday = monday;
        Tuesday = tuesday;
        Wednesday = wednesday;
        Thursday = thursday;
        Friday = friday;
    }
}
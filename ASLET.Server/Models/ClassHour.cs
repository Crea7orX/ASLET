namespace ASLET.Server.Models;

public class ClassHour
{
    public string HourId { get; set; }
    public string ClassId { get; set; }
    public SchoolClass Class { get; set; }
    public Hour Hour { get; set; }
}
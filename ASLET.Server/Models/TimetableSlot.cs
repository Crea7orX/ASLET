namespace ASLET.Server.Models;

public class TimetableSlot : Entity
{
    public int Lesson { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public string ClassId { get; set; }
    public string TeacherId { get; set; }
    public string HourId { get; set; }
    public Hour Hour { get; set; }
    public List<SchoolClass> Classes { get; set; }
}
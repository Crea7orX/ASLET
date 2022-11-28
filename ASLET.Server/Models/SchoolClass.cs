namespace ASLET.Server.Models;

public class SchoolClass : Entity
{
    public byte Grade { get; set; }
    public char Letter { get; set; }
    public List<ClassHour> ClassHours { get; set; }
    public List<TimetableSlot> TimetableSlots { get; set; }
}
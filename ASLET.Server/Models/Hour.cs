namespace ASLET.Server.Models;

public class Hour : Entity
{
    public string Title { get; set; }
    public List<TimetableSlot> Slots { get; set; }
    public Teacher Teacher { get; set; }
    public List<ClassHour> Classes { get; set; }
}
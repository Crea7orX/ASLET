namespace ASLET.Models;

public class TimetableSlotModel
{
    public string Class { get; set; }
    public ProfessorModel Teacher { get; set; }
    public SubjectClassModel Subject { get; set; }
    public RoomModel Room { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }

    public TimetableSlotModel(string @class, ProfessorModel teacher, SubjectClassModel subject)
    {
        Class = @class;
        Teacher = teacher;
        Subject = subject;
    }

    public override string ToString()
    {
        return Class + " - " + Teacher.Name + " - " + Subject.SubjectModel.Name + " - " + Room.Name;
    }
    
    public string ClassToString()
    {
        return Teacher.Name + " - " + Subject.SubjectModel.Name + " - " + Room.Name;
    }
    
    public string TeacherToString()
    {
        return Class + " - " + Subject.SubjectModel.Name + " - " + Room.Name;
    }

    public string RoomToString()
    {
        return Class + " - " + Teacher.Name + " - " + Subject.SubjectModel.Name;
    }
}
namespace ASLET.Models;

public class HourModel
{
    public byte Grade { get; }
    public char Letter { get; }
    public string? TeacherName { get; }
    public string? SubjectName { get; }
    public byte HoursAWeek { get; }

    public HourModel(byte grade, char letter, string? teacherName, string? subjectName, byte hoursAWeek)
    {
        Grade = grade;
        Letter = letter;
        TeacherName = teacherName;
        SubjectName = subjectName;
        HoursAWeek = hoursAWeek;
    }
}
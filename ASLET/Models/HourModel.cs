using System;
using ASLET.Services.Objects;

namespace ASLET.Models;

public class HourModel
{
    public Guid HourId { get; }
    public Guid ClassId { get; }
    public byte Grade { get; }
    public char? Letter { get; }
    public Guid TeacherId { get; }
    public string? TeacherName { get; }
    public Guid SubjectId { get; }
    public string? SubjectName { get; }
    public byte HoursAWeek { get; }

    public HourModel(Guid classId, byte grade, char? letter, Guid teacherId, string? teacherName, Guid subjectId, string? subjectName, byte hoursAWeek)
    {
        HourId = Guid.NewGuid();
        ClassId = classId;
        Grade = grade;
        Letter = letter;
        TeacherId = teacherId;
        TeacherName = teacherName;
        SubjectId = subjectId;
        SubjectName = subjectName;
        HoursAWeek = hoursAWeek;
    }

    public string ClassToString()
    {
        return Grade.ToString() + Letter;
    }

    public SubjectExample GetSubject()
    {
        return new SubjectExample(SubjectId, TeacherId, SubjectName, TeacherName);
    }
}
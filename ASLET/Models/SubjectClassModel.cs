using System;
using System.Collections.Generic;
using System.Linq;

namespace ASLET.Models;

public class SubjectClassModel : IComparable<SubjectClassModel>
{
    private static int _nextClassId = 0;

    // Initializes class object
    public SubjectClassModel(TeacherModel teacherModel, SubjectModel subjectModel, bool requiresLab, int duration,
        params StudentsGroupModel[] groups)
    {
        Id = _nextClassId++;
        TeacherModel = teacherModel;
        SubjectModel = subjectModel;
        NumberOfSeats = 0;
        LabRequired = requiresLab;
        Duration = duration;
        Groups = new List<StudentsGroupModel>();

        // bind teacher to class
        TeacherModel.AddCourseClass(this);

        // bind student groups to class
        foreach (StudentsGroupModel group in groups)
        {
            group.AddClass(this);
            Groups.Add(group);
            NumberOfSeats += group.NumberOfStudents;
        }
    }

    // Returns TRUE if another class has one or overlapping student groups.
    public bool GroupsOverlap(SubjectClassModel c)
    {
        return Groups.Intersect(c.Groups).Any();
    }

    // Returns TRUE if another class has same teacher.
    public bool ProfessorOverlaps(SubjectClassModel c)
    {
        return TeacherModel.Equals(c.TeacherModel);
    }

    public int CompareTo(SubjectClassModel other)
    {
        if (other == null)
            return -1;
        return other.Id - Id;
    }

    // Returns class ID - automatically assigned
    public int Id { get; set; }

    // Return pointer to teacher who teaches
    public TeacherModel TeacherModel { get; set; }

    // Return pointer to course to which class belongs
    public SubjectModel SubjectModel { get; set; }

    // Returns reference to list of student groups who attend class
    public List<StudentsGroupModel> Groups { get; set; }

    // Returns number of seats (students) required in room
    public int NumberOfSeats { get; set; }

    // Returns TRUE if class requires computers in room.
    public bool LabRequired { get; set; }

    // Returns duration of class in hours
    public int Duration { get; set; }

    // Restarts ID assigments
    public static void RestartIDs()
    {
        _nextClassId = 0;
    }
}
using System;
using System.Collections.Generic;

namespace ASLET.Models;

public class TeacherModel
{
    private static int _nextTeacherId = 0;
    // Initializes teacher data
    public TeacherModel(string name, string uniqueId = "")
    {
        UniqueId = uniqueId;
        Id = _nextTeacherId++;
        Name = name;
        CourseClasses = new List<SubjectClassModel>();
    }

    // Bind teacher to course
    public void AddCourseClass(SubjectClassModel subjectClassModel)
    {
        CourseClasses.Add(subjectClassModel);
    }

    public override bool Equals(object obj)
    {
        return obj is TeacherModel teacher &&
               Id == teacher.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public string UniqueId { get; set; }

    // Returns teacher's ID
    public int Id { get; set; }

    // Returns teacher's name
    public string Name { get; set; }

    // Returns reference to list of classes that teacher teaches
    public List<SubjectClassModel> CourseClasses { get; set; }
}
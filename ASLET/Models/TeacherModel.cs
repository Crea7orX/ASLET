using System;
using System.Collections.Generic;

namespace ASLET.Models;

public class ProfessorModel
{
    private static int _nextTeacherId = 0;
    // Initializes teacher data
    public ProfessorModel(string name)
    {
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
        return obj is ProfessorModel teacher &&
               Id == teacher.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    // Returns teacher's ID
    public int Id { get; set; }

    // Returns teacher's name
    public string Name { get; set; }

    // Returns reference to list of classes that teacher teaches
    public List<SubjectClassModel> CourseClasses { get; set; }
}
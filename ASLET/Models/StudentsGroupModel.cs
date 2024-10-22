﻿using System;
using System.Collections.Generic;

namespace ASLET.Models;

public class StudentsGroupModel
{
    private static int _nextClassId = 0;
    // Initializes student group data
    public StudentsGroupModel(byte grade, char letter, int numberOfStudents)
    {
        Id = _nextClassId++;
        Name = grade.ToString() + letter;
        Grade = grade;
        Letter = letter;
        NumberOfStudents = numberOfStudents;
        CourseClasses = new List<SubjectClassModel>();
    }

    // Bind group to class
    public void AddClass(SubjectClassModel subjectClassModel)
    {			
        CourseClasses.Add(subjectClassModel);
    }

    public override bool Equals(object obj)
    {
        return obj is StudentsGroupModel group &&
               Id == group.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    // Returns student group ID
    public int Id { get; set; }

    // Returns name of student group
    public string Name { get; set; }
    public byte Grade { get; }
    public char? Letter { get; }

    // Returns number of students in group
    public int NumberOfStudents { get; set; }

    // Returns reference to list of classes that group attends
    public List<SubjectClassModel> CourseClasses { get; set; }
}
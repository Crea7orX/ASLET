using System;
using System.Collections.Generic;
using ASLET.Services.Objects;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public class Class
{
    public Guid Id { get; }

    public List<SubjectExample> Subjects { get; }

    public Dictionary<SubjectExample, int> SubjectPlan { get; }

    // public List TODO CONDITION
    public SubjectExample[,] Schedule { get; }
    public string Name { get; }

    public Class(Guid id, string name)
    {
        Id = id;
        Name = name;
        SubjectPlan = new Dictionary<SubjectExample, int>();
        Subjects = new List<SubjectExample>();
        Schedule = new SubjectExample[5, 7];
        ClearSchedule();
    }

    public void ClearSchedule()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Schedule[i, j] = new SubjectExample();
            }
        }
    }

    public void AddSubject(SubjectExample subject, int times)
    {
        DictionaryUtils.Put(SubjectPlan, subject, times);
        Subjects.Add(subject);
    }

    public void RemoveSubject(SubjectExample subject, int times)
    {
        // if (SubjectPlan[subject] == times)
        // {
        //     SubjectPlan.Remove(subject);
        // }
        // else
        // {
            SubjectPlan[subject] -= times;
        // }
        Subjects.Remove(subject);
    }
}
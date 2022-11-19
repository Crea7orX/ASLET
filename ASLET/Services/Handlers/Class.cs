using System.Collections.Generic;
using ASLET.Services.Objects;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public class Class
{
    public List<SubjectExample> Subjects { get; }

    public Dictionary<SubjectExample, int> SubjectPlan { get; }

    // public List TODO CONDITION
    public SubjectExample[,] Schedule { get; }
    public string Name { get; }

    public Class(string name)
    {
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

    public void RemoveSubject(SubjectExample subject)
    {
        SubjectPlan.Remove(subject);
        Subjects.Remove(subject);
    }
}
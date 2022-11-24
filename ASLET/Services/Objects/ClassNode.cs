using System;
using System.Collections.Generic;
using System.Linq;
using ASLET.Services.Utils;

namespace ASLET.Services.Objects;

public class ClassNode
{
    public Guid Id { get; }

    public List<DayNode> Days { get; }
    public string Name { get; }
    private Dictionary<SubjectExample, int> SubjectsPlan { get; }
    private Dictionary<SubjectExample, int> CurrentOccurrence { get; }
    private Dictionary<SubjectExample, object> Properties { get; set; } // TODO

    public ClassNode(Guid id, string name)
    {
        Id = id;
        Days = new List<DayNode>();
        SubjectsPlan = new Dictionary<SubjectExample, int>();
        CurrentOccurrence = new Dictionary<SubjectExample, int>();
        Properties = new Dictionary<SubjectExample, object>();
        Name = name;

        for (int i = 0; i < 5; i++)
        {
            Days.Add(new DayNode(i));
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (i != 4)
                {
                    Days[i].Hours[j].NextDayConnection = Days[i + 1];
                }

                if (i != 0)
                {
                    Days[i].Hours[j].PreviousDayConnection = Days[i - 1];
                }

                Days[i].Hours[j].SameDayConnection = Days[i];
            }
        }
    }

    public void AddOccurrence(SubjectExample subjectExample)
    {
        DictionaryUtils.Put(CurrentOccurrence, subjectExample, CurrentOccurrence[subjectExample] + 1);
    }

    public void RemoveOccurrence(SubjectExample subjectExample)
    {
        DictionaryUtils.Put(CurrentOccurrence, subjectExample, CurrentOccurrence[subjectExample] - 1);
    }

    // TODO LATER IMPLEMENTATION
    public bool CompleteConditions(HourNode node)
    {
        return true;
    }

    public bool IsReady()
    {
        SubjectExample[] keys = SubjectsPlan.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            if (SubjectsPlan[keys[i]] != CurrentOccurrence[keys[i]])
            {
                return false;
            }
        }

        return true;
    }

    public int GetOccurrence(SubjectExample subjectExample)
    {
        return CurrentOccurrence[subjectExample];
    }

    public int GetSubjectPlan(SubjectExample subjectExample)
    {
        return SubjectsPlan[subjectExample];
    }

    public void AddSubject(SubjectExample subjectExample, int times)
    {
        SubjectsPlan.Add(subjectExample, times);
        CurrentOccurrence.Add(subjectExample, 0);
        for (int i = 0; i < Days.Count; i++)
        {
            for (int j = 0; j < Days[i].Hours.Count; j++)
            {
                Days[i].Hours[j].Domain.Add(subjectExample);
            }
        }
    }
    //
    // public void addCondition(ConditionDescription condition)
    // {
    //     // TODO SWITCH CASE
    // }

    public override bool Equals(object? obj)
    {
        return ((ClassNode)obj).Id.Equals(Id);
    }

    // TODO MAYBE LATER
    public List<HourNode> GetHours()
    {
        List<HourNode> hours = new List<HourNode>();
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                hours.Add(Days[i].Hours[j]);
            }
        }

        return hours;
    }
}
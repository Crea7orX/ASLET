using System.Collections;
using ASLET.Utils;

namespace ASLET.Objects;

public class ClassNode
{
    public List<DayNode> Days { get; private set; }
    public String Name { get; private set; }
    private Dictionary<SubjectExample, int> SubjectsPlan { get; set; }
    private Dictionary<SubjectExample, int> CurrentOccurrence { get; set; }
    private Dictionary<SubjectExample, object> Properties { get; set; }

    public ClassNode(String name)
    {
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
                    Days[i].Hours[j].nextDayConnection = Days[i + 1];
                }

                if (i != 0)
                {
                    Days[i].Hours[j].previousDayConnection = Days[i - 1];
                }

                Days[i].Hours[j].sameDayConnection = Days[i];
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
        return ((ClassNode)obj).Name.Equals(Name);
    }

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
using System.Drawing;
using ASLET.Handlers;
using ASLET.Objects.Conditions;
using ASLET.Utils;

namespace ASLET.Objects;

public class HourNode
{
    public SubjectExample SubjectExample { get; private set; }
    public readonly List<HourNode> sameTimeConnection;
    public List<SubjectExample> Domain { get; private set; }
    public DayNode sameDayConnection, nextDayConnection, previousDayConnection;
    public HourNode nextHour, previousHour;
    public ClassNode nextClass, previousClass, sameClass;
    private readonly List<Condition> condition;
    private readonly List<HourNode> _setSingletons;
    private readonly List<Point> _weekReductions;
    public bool set { get; private set; } = false;
    private int _hour, _day;
    public Point weekPoint;

    public HourNode(int day, int hour)
    {
        SubjectExample = new SubjectExample();
        sameTimeConnection = new List<HourNode>();
        Domain = new List<SubjectExample>();
        condition = new List<Condition>();
        _setSingletons = new List<HourNode>();
        _weekReductions = new List<Point>();
        _day = day;
        _hour = hour;
        weekPoint = new Point(day, hour);
    }

    public void SetConsecutiveCondition(SubjectExample subjectExample)
    {
        condition.Add(new MustBeConsecutiveCondition(this, subjectExample));
    }

    // TODO
    public bool CompleteCondition()
    {
        foreach (var currentCondition in condition)
        {
            if (!currentCondition.Complete())
            {
                return false;
            }
        }

        return true;
    }

    public override string ToString()
    {
        return SubjectExample.Subject;
    }

    public HourNode GetNext()
    {
        if (nextHour != null)
        {
            return nextHour;
        }
        else if (nextDayConnection != null)
        {
            return nextDayConnection.Hours[0];
        }
        else
        {
            return null;
        }
    }

    public void SetSubject(SubjectExample subjectExample)
    {
        if (sameClass.GetOccurrence(subjectExample) == sameClass.GetSubjectPlan(subjectExample))
        {
            return;
        }

        SubjectExample = subjectExample;
        sameClass.AddOccurrence(subjectExample);
        set = true;

        PropagateThroughClassesAndReduceDomain();
        PropagateThroughDayAndReduceDomain();
        PropagateThroughWeekAndReduceDomain();
        PropagateThroughSingletonDomains();
    }

    public void SetManual(SubjectExample subjectExample)
    {
        SubjectExample = subjectExample;
        sameClass.AddOccurrence(subjectExample);
        if (sameClass.GetOccurrence(subjectExample) > sameClass.GetSubjectPlan(subjectExample))
        {
            sameClass.RemoveOccurrence(subjectExample);
            SubjectExample = new SubjectExample();
        }

        set = true;
        PropagateThroughClassesAndReduceDomain();
        PropagateThroughDayAndReduceDomain();
    }

    public void UnSet()
    {
        if (!set)
        {
            return;
        }

        set = false;
        sameClass.RemoveOccurrence(SubjectExample);
        foreach (Condition currentCondition in condition)
        {
            currentCondition.UnComplete();
        }

        RestoreClassesDomain();
        RestoreDayDomain();
        RestoreWeekDomain();
        RestoreSingletonDomains();
        _setSingletons.Clear();
        _weekReductions.Clear();
        SubjectExample = new SubjectExample();
    }

    private void PropagateThroughDayAndReduceDomain()
    {
        List<HourNode> nodesOfDay = sameDayConnection.Hours;
        foreach (HourNode node in nodesOfDay)
        {
            if (!node.set)
            {
                node.Domain.Remove(SubjectExample);
            }
        }
    }

    private void PropagateThroughClassesAndReduceDomain()
    {
        for (int i = 0; i < sameTimeConnection.Count; i++)
        {
            if (!sameTimeConnection[i].set)
            {
                sameTimeConnection[i].Domain.Remove(SubjectExample);
            }
        }
    }

    private void PropagateThroughSingletonDomains()
    {
        List<HourNode> allNodes = ScheduleFabric.algControll.State;
        foreach (HourNode node in allNodes)
        {
            if (node.IsSingletonDomain() && !node.set && !node.sameClass.IsReady())
            {
                _setSingletons.Add(node);
                node.SetSubject(node.Domain[0]);
            }
        }
    }

    private void PropagateThroughWeekAndReduceDomain()
    {
        if (sameClass.GetOccurrence(SubjectExample) == sameClass.GetSubjectPlan(SubjectExample))
        {
            foreach (DayNode day in sameClass.Days)
            {
                foreach (HourNode hour in day.Hours)
                {
                    if (!hour.set)
                    {
                        if (hour.Domain.Remove(SubjectExample))
                        {
                            _weekReductions.Add(hour.weekPoint);
                        }
                    }
                }
            }
        }
    }

    private void RestoreWeekDomain()
    {
        if (sameClass.GetOccurrence(SubjectExample) == sameClass.GetSubjectPlan(SubjectExample))
        {
            foreach (DayNode day in sameClass.Days)
            {
                foreach (HourNode hour in day.Hours)
                {
                    if (_weekReductions.Contains(hour.weekPoint))
                    {
                        hour.Domain.Add(SubjectExample);
                    }
                }
            }
        }
    }

    private void RestoreSingletonDomains()
    {
        foreach (HourNode node in _setSingletons)
        {
            node.UnSet();
        }
    }

    private void RestoreClassesDomain()
    {
        for (int i = 0; i < sameTimeConnection.Count; i++)
        {
            if (!sameTimeConnection[i].set)
            {
                sameTimeConnection[i].Domain.Add(SubjectExample);
            }
        }
    }

    private void RestoreDayDomain()
    {
        List<HourNode> nodesOfDay = sameDayConnection.Hours;
        foreach (HourNode node in nodesOfDay)
        {
            if (!node.set && node != this)
            {
                node.Domain.Add(SubjectExample);
            }
        }
    }

    public void SortByHardness()
    {
        Domain.Sort((a, b) =>
        {
            int hard1 = GlobalSpace.SubjectController.SubjectsDictionary[a.Subject].Hardness;
            int hard2 = GlobalSpace.SubjectController.SubjectsDictionary[b.Subject].Hardness;
            if (hard1 > hard2)
            {
                return -1;
            }

            if (hard1 < hard2)
            {
                return 1;
            }

            return 0;
        });
    }

    public void ShuffleDomain(bool sorted)
    {
        if (!sorted)
        {
            Domain.Shuffle();
        }
        else
        {
            ShuffleSorted();
        }
    }

    private void ShuffleSorted()
    {
        Dictionary<int, List<SubjectExample>> dictionary = new Dictionary<int, List<SubjectExample>>();
        foreach (SubjectExample element in Domain)
        {
            int hardness = GlobalSpace.SubjectController.SubjectsDictionary[element.Subject].Hardness;
            if (!dictionary.ContainsKey(hardness))
            {
                DictionaryUtils.Put(dictionary, hardness, new List<SubjectExample>());
            }

            dictionary[hardness].Add(element);
        }

        foreach (List<SubjectExample> element in dictionary.Values)
        {
            element.Shuffle();
        }

        Domain.Clear();

        for (int i = 3; i >= 1; i--)
        {
            foreach (KeyValuePair<int, List<SubjectExample>> keyValuePair in dictionary)
            {
                if (keyValuePair.Key == i)
                {
                    Domain.AddRange(keyValuePair.Value);
                }
            }
        }
    }

    private bool IsSingletonDomain()
    {
        return Domain.Count == 1;
    }
}
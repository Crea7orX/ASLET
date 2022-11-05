using ASLET.Objects;

namespace ASLET.Utils;

public class Controller
{
    public List<ClassNode> Classes { get; private set; }
    public List<HourNode> State { get; private set; }

    public Controller()
    {
        Classes = new List<ClassNode>();
        State = new List<HourNode>();
    }

    private bool AllReady()
    {
        for (int i = 0; i < Classes.Count; i++)
        {
            if (!Classes[i].IsReady())
            {
                return false;
            }
        }

        return true;
    }

    private bool Assign(IEnumerator<HourNode> nodes, int index = 0)
    {
        if (AllReady())
        {
            return true;
        }

        for (int i = 0; i < index; i++)
        {
            nodes.MoveNext();
        }

        if (index == State.Count)
        {
            if (AllReady())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        HourNode node = State[index];
        if (node.sameClass.IsReady() && node.nextClass != null)
        {
            return Assign(State.GetEnumerator(), State.IndexOf(node.nextClass.Days[0].Hours[0]));
        }

        if (node.set)
        {
            return Assign(State.GetEnumerator(), index + 1);
        }

        if (node.previousHour != null && !node.previousHour.set)
        {
            return false;
        }

        if (node.Domain.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < node.Domain.Count; i++)
        {
            SubjectExample subjectExample = node.Domain[i];
            node.SetSubject(subjectExample);
            if (CheckForHoles())
            {
                // TODO
            }

            if (!node.sameClass.IsReady())
            {
                if (Assign(State.GetEnumerator(), index + 1))
                {
                    return true;
                }
            }
            else
            {
                if (node.nextClass != null)
                {
                    if (Assign(State.GetEnumerator(), State.IndexOf(node.nextClass.Days[0].Hours[0])))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            node.UnSet();
        }

        return false;
    }

    public void AddClass(ClassNode studentsClass)
    {
        Classes.Add(studentsClass);
        if (Classes.Count > 1)
        {
            int lastIndex = Classes.Count - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        Classes[i].Days[j].Hours[k].sameTimeConnection.Add(Classes[lastIndex].Days[j].Hours[k]);
                        Classes[lastIndex].Days[j].Hours[k].sameTimeConnection.Add(Classes[i].Days[j].Hours[k]);
                    }
                }
            }
        }
    }

    private List<SubjectExample[,]> GetData()
    {
        List<SubjectExample[,]> data = new List<SubjectExample[,]>();
        for (int i = 0; i < Classes.Count; i++)
        {
            SubjectExample[,] week = new SubjectExample[7, 5];
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    week[k, j] = Classes[i].Days[j].Hours[k].SubjectExample;
                }
            }

            data.Add(week);
        }

        return data;
    }

    public void Print()
    {
        List<SubjectExample[,]> data = GetData();
        for (int i = 0; i < data.Count; i++)
        {
            SubjectExample[,] week = data[i];
            for (int j = 0; j < week.GetLength(0); j++)
            {
                for (int k = 0; k < week.GetLength(1); k++)
                {
                    Object subject = week[j, k].Subject;
                    Console.Write((subject + "|").PadRight(40));
                }

                Console.WriteLine();
            }

            Console.WriteLine("----------------");
        }

        Console.WriteLine("~~~~~~~~~~~~~~~~");
    }

    public void SetUpClasses()
    {
        for (int i = 0; i < Classes.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    Classes[i].Days[j].Hours[k].sameClass = Classes[i];
                    if (i != Classes.Count - 1)
                    {
                        Classes[i].Days[j].Hours[k].nextClass = Classes[i + 1];
                    }

                    if (i != 0)
                    {
                        Classes[i].Days[j].Hours[k].previousClass = Classes[i - 1];
                    }
                }
            }
        }
    }

    public void Make()
    {
        for (int i = 0; i < Classes.Count; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    State.Add(Classes[i].Days[k].Hours[j]);
                    if (true)
                    {
                        Classes[i].Days[k].Hours[j].SortByHardness();
                    }

                    if (true)
                    {
                        Classes[i].Days[k].Hours[j].ShuffleDomain(true);
                    }
                }
            }
        }

        Console.WriteLine("START GENERATING");
        Console.WriteLine(!Assign(State.GetEnumerator()) ? "FAIL" : "SUCCESS");
        Print();
    }

    private bool CheckForHoles()
    {
        foreach (ClassNode classNode in Classes)
        {
            foreach (DayNode dayNode in classNode.Days)
            {
                foreach (HourNode hourNode in dayNode.Hours)
                {
                    if (!hourNode.set)
                    {
                        if (hourNode.previousHour == null && hourNode.nextHour.set)
                        {
                            return true;
                        }

                        if (hourNode.nextHour != null && (hourNode.nextHour.set && hourNode.previousHour.set))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
using System;
using System.Collections.Generic;
using ASLET.Models;
using ASLET.Services.Objects;

namespace ASLET.Services.Utils;

public class Controller
{
    public List<ClassNode> Classes { get; }
    public List<HourNode> State { get; }

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

    private bool Assign(int index = 0)
    {
        if (AllReady())
        {
            return true;
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
        if (node.SameClass.IsReady() && node.NextClass != null)
        {
            return Assign(State.IndexOf(node.NextClass.Days[0].Hours[0]));
        }

        if (node.Set)
        {
            return Assign(index + 1);
        }

        if (node.PreviousHour != null && !node.PreviousHour.Set)
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

            if (!node.SameClass.IsReady())
            {
                if (Assign(index + 1))
                {
                    return true;
                }
            }
            else
            {
                if (node.NextClass != null)
                {
                    if (Assign(State.IndexOf(node.NextClass.Days[0].Hours[0])))
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
                        Classes[i].Days[j].Hours[k].SameTimeConnection.Add(Classes[lastIndex].Days[j].Hours[k]);
                        Classes[lastIndex].Days[j].Hours[k].SameTimeConnection.Add(Classes[i].Days[j].Hours[k]);
                    }
                }
            }
        }
    }

    // TODO LATER IMPLEMENTATION
    public List<TimetableModel> GetData(string className)
    {
        List<TimetableModel> data = new List<TimetableModel>();
        for (int i = 0; i < Classes.Count; i++)
        {
            if (Classes[i].Name != className) continue;
            for (int k = 0; k < 7; k++)
            {
                string[] hours = new string[5];
                for (int j = 0; j < 5; j++)
                {
                    string subject = Classes[i].Days[j].Hours[k].SubjectExample.Subject + " - " + Classes[i].Days[j].Hours[k].SubjectExample.Teacher;
                    if (subject == "неизвестно - неизвестно") subject = "";
                    hours[j] = subject;
                }
                data.Add(new TimetableModel(k.ToString(), hours[0], hours[1], hours[2], hours[3], hours[4]));
            }

        }

        return data;
    }

    public void SetUpClasses()
    {
        for (int i = 0; i < Classes.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    Classes[i].Days[j].Hours[k].SameClass = Classes[i];
                    if (i != Classes.Count - 1)
                    {
                        Classes[i].Days[j].Hours[k].NextClass = Classes[i + 1];
                    }

                    if (i != 0)
                    {
                        Classes[i].Days[j].Hours[k].PreviousClass = Classes[i - 1];
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

        Assign();
        Console.WriteLine("DONE!!!!!!!!");
    }

    private bool CheckForHoles()
    {
        foreach (ClassNode classNode in Classes)
        {
            foreach (DayNode dayNode in classNode.Days)
            {
                foreach (HourNode hourNode in dayNode.Hours)
                {
                    if (!hourNode.Set)
                    {
                        if (hourNode.PreviousHour == null && hourNode.NextHour.Set)
                        {
                            return true;
                        }

                        if (hourNode.NextHour != null && (hourNode.NextHour.Set && hourNode.PreviousHour.Set))
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
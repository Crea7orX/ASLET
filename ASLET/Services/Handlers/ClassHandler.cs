using System;
using System.Collections.Generic;
using System.Drawing;
using ASLET.Services.Objects;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public class ClassHandler : IHandler
{
    public Dictionary<string, Class> Classes { get; }
    private static readonly SubjectExample EmptySubject = new SubjectExample();
    private static readonly Random Random = new Random();

    public ClassHandler()
    {
        Classes = new Dictionary<string, Class>();
    }

    public void Add(string name)
    {
        DictionaryUtils.Put(Classes, name, new Class(name));
    }

    public void Remove(string name)
    {
        Classes.Remove(name);
    }

    public bool Contains(string name)
    {
        // TODO ?????????? VALUE
        return Classes.ContainsKey(name);
    }

    public void ClearAllSchedules()
    {
        foreach (Class schoolClass in Classes.Values)
        {
            schoolClass.ClearSchedule();
        }
    }

    public void ClearSchedule(string schoolClass)
    {
        Classes[schoolClass].ClearSchedule();
    }

    public void AddSubject(string schoolClass, SubjectExample subject, int times)
    {
        Classes[schoolClass].AddSubject(subject, times);
    }

    // TODO ADD CONDITION

    public void SetUpController(Controller controller, List<Class> classes)
    {
        foreach (Class schoolClass in classes)
        {
            string name = schoolClass.Name;
            ClassNode node = new ClassNode(name);
            foreach (KeyValuePair<SubjectExample, int> subject in schoolClass.SubjectPlan)
            {
                node.AddSubject(subject.Key, subject.Value);
            }

            // TODO CONDITION
            controller.AddClass(node);
        }

        controller.SetUpClasses();
        Class[] classArr = new Class[classes.Count];
        // TODO POSSIBLE REFERENCE BUGS
        Array.Copy(classes.ToArray(), classArr, classes.Count);
        // classes.ToArray().CopyTo(classArr, 0);
        for (int i = 0; i < classes.Count; i++)
        {
            SubjectExample[,] schedule = classArr[i].Schedule;
            for (int j = 0; j < schedule.GetLength(0); j++)
            {
                for (int k = 0; k < schedule.GetLength(1); k++)
                {
                    SubjectExample subject = schedule[j, k];
                    if (subject != null && !subject.Equals(EmptySubject))
                    {
                        controller.Classes[i].Days[j].Hours[k].SetManual(subject);
                    }
                }
            }
        }
    }

    public void GetReadySchedule(Controller controller)
    {
        for (int i = 0; i < controller.Classes.Count; i++)
        {
            ClassNode classNode = controller.Classes[i];
            SubjectExample[,] schedule = Classes[classNode.Name].Schedule;
            for (int j = 0; j < classNode.Days.Count; j++)
            {
                DayNode dayNode = classNode.Days[j];
                for (int k = 0; k < dayNode.Hours.Count; k++)
                {
                    HourNode hourNode = dayNode.Hours[k];
                    schedule[j, k].Subject = hourNode.SubjectExample.Subject;
                    schedule[j, k].Teacher = hourNode.SubjectExample.Teacher;
                }
            }
        }
    }

    public string GetTeacher(string schoolClass, string subject)
    {
        foreach (SubjectExample subjectExample in Classes[schoolClass].Subjects)
        {
            if (subjectExample.Subject.Equals(subject))
            {
                return subjectExample.Teacher;
            }
        }

        return null;
    }

    public void SetSubjectWithNoChecks(string schoolClass, int day, int hour, SubjectExample selectedValue)
    {
        Classes[schoolClass].Schedule[day, hour] = selectedValue;
    }

    public string SetSubject(string schoolClass, int day, int hour, SubjectExample selectedValue)
    {
        if (selectedValue.Equals(EmptySubject))
        {
            Classes[schoolClass].Schedule[day, hour] = selectedValue;
            return null;
        }

        SubjectExample[,] schedule = Classes[schoolClass].Schedule;
        int count = 1;
        for (int i = 0; i < schedule.GetLength(0); i++)
        {
            for (int j = 0; j < schedule.GetLength(1); j++)
            {
                SubjectExample subjectExample = schedule[i, j];
                if (subjectExample.Equals(selectedValue))
                {
                    count++;
                }
            }
        }

        if (count > Classes[schoolClass].SubjectPlan[selectedValue])
        {
            return "TOO MANY SUBJECTS IN WEEK";
        }

        for (int i = 0; i < Classes.Count; i++)
        {
            SubjectExample subject = Classes[schoolClass].Schedule[day, hour];
            if (!subject.Equals(EmptySubject) && subject.Equals(selectedValue))
            {
                return "SAME SUBJECT IN ANOTHER CLASS";
            }
        }

        Classes[schoolClass].Schedule[day, hour] = selectedValue;
        return null;
    }

    private void Swap(Class schoolClass, Point subjectLocation1, Point subjectLocation2)
    {
        SubjectExample subject1 = schoolClass.Schedule[subjectLocation1.X, subjectLocation1.Y];
        SubjectExample subject2 = schoolClass.Schedule[subjectLocation2.X, subjectLocation2.Y];
        SubjectExample subjectTemp = new SubjectExample(subject1.Subject, subject1.Teacher);
        subject1.Subject = subject2.Subject;
        subject1.Teacher = subject2.Teacher;
        subject2.Subject = subjectTemp.Subject;
        subject2.Teacher = subjectTemp.Teacher;
    }

    // TODO
    public void Randomize()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int newIndex = Random.Next(7);
                    bool success = true;
                    foreach (Class schoolClass in Classes.Values)
                    {
                        if (schoolClass.Schedule[i, j].Equals(SubjectExample.Empty) ||
                            schoolClass.Schedule[i, newIndex].Equals(SubjectExample.Empty))
                        {
                            success = false;
                            break;
                        }
                    }

                    if (!success)
                    {
                        continue;
                    }

                    foreach (Class schoolClass in Classes.Values)
                    {
                        Swap(schoolClass, new Point(i, j), new Point(i, newIndex));
                    }
                }
            }
        }
    }
}
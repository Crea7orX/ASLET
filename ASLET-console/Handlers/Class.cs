using ASLET.Objects;
using ASLET.Utils;

namespace ASLET.Handlers;

public class Class
{
    public List<SubjectExample> Subjects { get; private set; }

    public Dictionary<SubjectExample, int> SubjectPlan { get; private set; }

    // public List TODO CONDITION
    public SubjectExample[,] Schedule { get; private set; }
    public string Name { get; private set; }

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
}
using System.Collections;

namespace ASLET.Objects;

public class DayNode
{
    public List<HourNode> Hours { get; private set; }

    public DayNode(int day)
    {
        Hours = new List<HourNode>();
        for (int i = 0; i < 7; i++)
        {
            Hours.Add(new HourNode(day, i));
        }

        for (int i = 0; i < 7; i++)
        {
            if (i != 6)
            {
                Hours[i].nextHour = Hours[i + 1];
            }

            if (i != 0)
            {
                Hours[i].previousHour = Hours[i - 1];
            }
        }
    }

    public bool Contains(SubjectExample subjectExample)
    {
        foreach (HourNode hour in this.Hours)
        {
            if (hour.Domain.Contains(null))
            {
                return true;
            }
        }

        return false;
    }
}
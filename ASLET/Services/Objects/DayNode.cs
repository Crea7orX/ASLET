using System.Collections.Generic;

namespace ASLET.Services.Objects;

public class DayNode
{
    public List<HourNode> Hours { get; }

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
                Hours[i].NextHour = Hours[i + 1];
            }

            if (i != 0)
            {
                Hours[i].PreviousHour = Hours[i - 1];
            }
        }
    }

    // TODO MAYBE LATER
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
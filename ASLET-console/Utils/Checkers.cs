using ASLET.Objects;

namespace ASLET.Utils;

public class Checkers
{
    public static bool CanTimetableBeGenerated(List<Lesson> lessons)
    {
        UInt16 maxADaySum = 0;
        UInt16 maxAWeekSum = 0;

        foreach (Lesson lesson in lessons)
        {
            maxADaySum += lesson.maxADay;
            maxAWeekSum += lesson.maxAWeek;
        }

        return maxAWeekSum >= maxADaySum;
    }

    public static bool IsThereAnyGaps()
    {
        foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
        {
            foreach (Class schoolClass in Timetable.timetable.Keys)
            {
                if (Timetable.timetable[schoolClass][day].Count == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
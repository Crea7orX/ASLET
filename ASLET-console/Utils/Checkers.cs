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
}
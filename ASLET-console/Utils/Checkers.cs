using ASLET.Objects;

namespace ASLET.Utils;

public class Checkers
{
    public static bool CanTimetableBeGenerated(List<Lesson> lessons)
    {
        foreach (Lesson lesson in lessons)
        {
            if(lesson.maxADay > lesson.maxAWeek) return false;
        }
        return true;
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
using System.Collections;
using ASLET.Objects;

namespace ASLET.Objects
{
    public enum DaysOfWeek
    {
        MONDAY = 1,
        TUESDAY = 2,
        WEDNESDAY = 3,
        THURSDAY = 4,
        FRIDAY = 5
    }

    public class Timetable
    {
        public static Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>> timetable = new();

        public static void AddScheduleForDay(DaysOfWeek day, List<Tuple<Lesson, Teacher>> lessons)
        {
            timetable.Add(day, lessons);
        }
    }
}
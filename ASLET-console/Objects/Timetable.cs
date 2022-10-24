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
        public static Dictionary<Class, Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>>> timetable = new();

        public static void AddScheduleForDay(Class schoolClass, DaysOfWeek day, List<Tuple<Lesson, Teacher>> lessons)
        {
            Console.WriteLine(schoolClass.className + " " + timetable.ContainsKey(schoolClass));
            if (!timetable.ContainsKey(schoolClass))
            {
                Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>> dictionary = new() { { day, lessons } };
                timetable.Add(schoolClass, dictionary);
            }
            else timetable[schoolClass].Add(day, lessons);
        }
    }
}
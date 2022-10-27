namespace ASLET.Objects
{
    public enum DaysOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5
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
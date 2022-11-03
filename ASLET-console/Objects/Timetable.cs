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
            if (!timetable.ContainsKey(schoolClass))
            {
                Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>> dictionary = new() { { day, lessons } };
                timetable.Add(schoolClass, dictionary);
            }
            else timetable[schoolClass].Add(day, lessons);
        }

        public static void RemoveScheduleForDay(Class schoolClass, DaysOfWeek day)
        {
            Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>>? hello = new();
            if (timetable.TryGetValue(schoolClass, out hello) && hello.ContainsKey(day))
                timetable[schoolClass][day].Clear();
        }
        
        public static void ImportScheduleForDay(Class schoolClass, DaysOfWeek day, List<Tuple<Lesson, Teacher>> lessons)
        {
            if (timetable[schoolClass].ContainsKey(day))
                timetable[schoolClass][day] = lessons;
            else
                AddScheduleForDay(schoolClass, day, lessons);
        }

        public static void ClearTimetable()
        {
            timetable.Clear();
        }
    }
}
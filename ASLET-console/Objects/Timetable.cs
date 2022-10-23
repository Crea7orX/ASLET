using ASLET.Objects;

namespace ASLET.Objects
{
    public enum DaysOfWeek{
        MONDAY = 1,
        TUESDAY = 2,
        WEDNESDAY = 3,
        THURSDAY = 4,
        FRIDAY = 5,
        SATURDAY = 6,
        SUNDAY = 7
    }
    
    public class Timetable
    {
        public static Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>> timetable = new();

        public static void AddScheduleForDay(DaysOfWeek day, List<Tuple<Lesson, Teacher>> lessons) {
            /* foreach (Tuple<Lesson, Teacher> currentLesson in lessons)
            {
                Console.Write(currentLesson.Item1.displayName.PadRight(60));
                Console.Write(currentLesson.Item2.name.PadRight(30));
                Console.Write(String.Join(", ", currentLesson.Item2.freeLessons).PadRight(60));
                int freeLessonsCount = 0;
                foreach (bool lesson in currentLesson.Item2.freeLessons)
                    if (lesson) freeLessonsCount++;
                Console.WriteLine(freeLessonsCount);
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------"); */
            timetable.Add(day, lessons);
        }
    }
}
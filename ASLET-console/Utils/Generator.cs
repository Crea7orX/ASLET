using ASLET.Objects;

namespace ASLET.Utils
{
    public class Generator
    {
        public List<Tuple<Lesson, Teacher>> schedule;
        private readonly List<Lesson> _lessons;
        private readonly List<Teacher> _teachers;
        private readonly List<Class> _classes;

        private readonly Random _random;
        private readonly Dictionary<Class, short> _classLessonsDiff;


        public Generator(List<Lesson> lessons, List<Teacher> teachers, List<Class> classes)
        {
            schedule = new List<Tuple<Lesson, Teacher>>();
            _lessons = lessons;
            _teachers = teachers;
            _classes = classes;
            _random = new Random();
            _classLessonsDiff = new();
        }

        public void GenerateForWeek()
        {
            Console.WriteLine(Checkers.CanTimetableBeGenerated(_lessons));
            if (!Checkers.CanTimetableBeGenerated(_lessons))
            {
                Console.WriteLine("Не може да бъде генерирана програма! ПРИЧИНА: НЯМА ДОСТАТЪЧНО СЕДМИЧНИ ЧАСОВЕ!");
                return;
            }

            foreach (Class schoolClass in _classes)
            {
                _classLessonsDiff.Add(schoolClass, (short)(Lesson.totalCount % 5));
            }

            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                foreach (Class schoolClass in _classes)
                {
                    GenerateForDay(schoolClass, GetLessonsForADay(schoolClass));
                    Timetable.AddScheduleForDay(schoolClass, day, schedule);
                    schedule = new List<Tuple<Lesson, Teacher>>();
                }

                foreach (Teacher teacher in _teachers)
                {
                    teacher.SetFreeLessons();
                }
            }
        }

        public void GenerateForDay(Class schoolClass, byte hours)
        {
            for (byte i = 1; i <= hours; i++)
            {
                schedule.Add(GenerateNextLesson(schoolClass, i));
            }
        }

        private Tuple<Lesson, Teacher> GenerateNextLesson(Class schoolClass, byte hour)
        {
            do
            {
                byte index = (byte)_random.Next(0, _lessons.Count);
                Lesson currentLesson = _lessons[index];
                byte lessonsCount = GetLessonCountDay(currentLesson);
                byte lessonsComplexity = GetComplexityForADay();
                if (lessonsCount >= currentLesson.maxADay && lessonsComplexity < 10) continue;
                lessonsCount = GetLessonCountWeek(schoolClass, currentLesson);
                if (lessonsCount >= currentLesson.maxAWeek) continue;
                Teacher? currentTeacher = GetFreeTeacher(currentLesson.subject, hour);
                if (currentTeacher != null)
                    return Tuple.Create(currentLesson, currentTeacher);
            } while (true);
        }

        private Teacher? GetFreeTeacher(string subject, byte hour)
        {
            foreach (Teacher teacher in _teachers)
            {
                if (teacher.subject != subject || !teacher.freeLessons[hour - 1]) continue;
                teacher.freeLessons[hour - 1] = false;
                return teacher;
            }

            return null;
        }

        private byte GetLessonCountDay(Lesson lesson)
        {
            byte count = 0;
            foreach (var scheduleLesson in schedule)
            {
                if (scheduleLesson.Item1.subject == lesson.subject)
                    count++;
            }

            return count;
        }

        private byte GetLessonCountWeek(Class schoolClass, Lesson lesson)
        {
            byte count = 0;
            if (Timetable.timetable.ContainsKey(schoolClass))
            {
                foreach (List<Tuple<Lesson, Teacher>> currentDay in Timetable.timetable[schoolClass].Values)
                {
                    foreach (Tuple<Lesson, Teacher> currentLesson in currentDay)
                    {
                        if (currentLesson.Item1.subject == lesson.subject)
                            count++;
                    }
                }
            }
            else return 0;

            return count;
        }

        private byte GetLessonsForADay(Class schoolClass)
        {
            byte count;

            if (_classLessonsDiff[schoolClass] > 0)
            {
                count = (byte)(Lesson.totalCount / 5 + 1);
                _classLessonsDiff[schoolClass]--;
            }
            else
                count = (byte)(Lesson.totalCount / 5);

            return count;
        }

		private byte GetComplexityForADay()
		{
            byte  totalComplexity = 0;
            foreach (var scheduleLesson in schedule)
            {
                    totalComplexity += (byte)scheduleLesson.Item1.complexity;
            }

            return totalComplexity;
		}
    }
}
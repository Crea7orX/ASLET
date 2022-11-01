using ASLET.Objects;

namespace ASLET.Utils
{
    public class Generator
    {
        private List<Tuple<Lesson, Teacher>> _schedule;
        private readonly List<Lesson> _lessons;
        private readonly List<Teacher> _teachers;
        private readonly List<Class> _classes;

        private readonly Random _random;
        private readonly Dictionary<Class, short> _classLessonsDiff;


        public Generator(List<Lesson> lessons, List<Teacher> teachers, List<Class> classes)
        {
            _schedule = new List<Tuple<Lesson, Teacher>>();
            _lessons = lessons;
            _teachers = teachers;
            _classes = classes;
            _random = new Random();
            _classLessonsDiff = new();
        }

        public void GenerateForWeek(bool fillGaps = true)
        {
            Console.WriteLine(Checkers.CanTimetableBeGenerated(_lessons));
            if (!fillGaps)
            {
                _schedule = new List<Tuple<Lesson, Teacher>>();
                _classLessonsDiff.Clear();
                Timetable.ClearTimetable();
            }

            if (!Checkers.CanTimetableBeGenerated(_lessons))
            {
                Console.WriteLine("Не може да бъде генерирана програма! ПРИЧИНА: НЯМА ДОСТАТЪЧНО СЕДМИЧНИ ЧАСОВЕ!");
                return;
            }

            foreach (Class schoolClass in _classes)
            {
                _classLessonsDiff.Add(schoolClass, (short)(Lesson.totalCountAWeek % 5));
            }

            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                foreach (Class schoolClass in _classes)
                {
                    GenerateForDay(schoolClass, day, GetLessonsForADay(schoolClass));
                    Timetable.AddScheduleForDay(schoolClass, day, _schedule);
                    _schedule = new List<Tuple<Lesson, Teacher>>();
                }

                foreach (Teacher teacher in _teachers)
                {
                    teacher.SetFreeLessons();
                }
            }

            FillTimetableGaps(fillGaps);
        }

        private void GenerateForDay(Class schoolClass, DaysOfWeek day, byte hours, byte times = 0,
            bool recursive = true)
        {
            for (byte i = 1; i <= hours; i++)
            {
                _schedule.Add(GenerateNextLesson(schoolClass, i));
            }

            if (_schedule.Contains(Tuple.Create(_lessons[^1], _teachers[^1])) && recursive)
            {
                if (times < 2)
                {
                    times++;
                    UpdateFreeLessonsForTeacher();
                    _schedule.Clear();
                    GenerateForDay(schoolClass, day, hours, times);
                }
                else
                {
                    _schedule.Clear();
                    Timetable.RemoveScheduleForDay(schoolClass, day);
                    if (day - 1 != 0) Timetable.RemoveScheduleForDay(schoolClass, day - 1);
                }
            }
            else if (_schedule.Contains(Tuple.Create(_lessons[^1], _teachers[^1])) && !recursive)
            {
                if (times < 6)
                {
                    times++;
                    UpdateFreeLessonsForTeacher();
                    _schedule.Clear();
                    GenerateForDay(schoolClass, day, hours, times, recursive);
                }
                else
                {
                    _schedule.Clear();
                    Timetable.RemoveScheduleForDay(schoolClass, day);
                }
            }
        }

        private Tuple<Lesson, Teacher> GenerateNextLesson(Class schoolClass, byte hour)
        {
            int failedAttempts = 0;
            do
            {
                byte index = (byte)_random.Next(0, _lessons.Count);
                Lesson currentLesson = _lessons[index];
                byte lessonsCount = GetLessonCountDay(currentLesson);
                if (lessonsCount >= currentLesson.maxADay)
                {
                    failedAttempts++;
                    continue;
                }

                lessonsCount = GetLessonCountWeek(schoolClass, currentLesson);
                if (lessonsCount >= currentLesson.maxAWeek)
                {
                    failedAttempts++;
                    continue;
                }

                byte lessonsComplexity = GetComplexityForADay();
                if (Lesson.totalComplexity - lessonsComplexity < 16)
                {
                    failedAttempts++;
                    continue;
                }

                Teacher? currentTeacher = GetFreeTeacher(currentLesson.subject, hour, schoolClass);
                if (currentTeacher != null)
                    return Tuple.Create(currentLesson, currentTeacher);
                failedAttempts++;
            } while (failedAttempts <= 60);

            return Tuple.Create(_lessons[^1], _teachers[^1]);
        }

        private void FillTimetableGaps(bool fillGaps)
        {
            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                UpdateFreeLessonsForTeacher(day);
                foreach (Class schoolClass in _classes)
                {
                    Console.WriteLine(Timetable.timetable[schoolClass][day].Count + " " + day);
                    if (Timetable.timetable[schoolClass][day].Count == 0)
                    {
                        GenerateForDay(schoolClass, day, GetLessonsForADay(schoolClass), 0, false);
                        Timetable.ImportScheduleForDay(schoolClass, day, _schedule);
                        _schedule = new List<Tuple<Lesson, Teacher>>();
                    }
                }

                foreach (Teacher teacher in _teachers)
                {
                    teacher.SetFreeLessons();
                }
            }

            if (fillGaps)
            {
                byte count = 0;
                while (Checkers.IsThereAnyGaps() && count < 6)
                {
                    GenerateForWeek(false);
                    count++;
                }
            }
        }

        private void UpdateFreeLessonsForTeacher(DaysOfWeek day)
        {
            foreach (Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>> schedule in Timetable.timetable.Values)
            {
                byte hour = 0;
                foreach (Tuple<Lesson, Teacher> lessonTeacherPair in schedule[day])
                {
                    if (!lessonTeacherPair.Item1.Equals(_lessons[^1]))
                        lessonTeacherPair.Item2.freeLessons[hour] = false;
                    hour++;
                }
            }
        }

        private void UpdateFreeLessonsForTeacher()
        {
            byte hour = 0;
            foreach (Tuple<Lesson, Teacher> lessonTeacherPair in _schedule)
            {
                if (!lessonTeacherPair.Item1.Equals(_lessons[^1]))
                    lessonTeacherPair.Item2.freeLessons[hour] = true;
                hour++;
            }
        }

        private Teacher? GetFreeTeacher(string subject, byte hour, Class schoolClass)
        {
            foreach (Teacher teacher in _teachers)
            {
                if (!teacher.attachedClasses.Contains(schoolClass)) continue;
                if (!teacher.TeachingSubject(subject) || !teacher.freeLessons[hour - 1]) continue;
                teacher.freeLessons[hour - 1] = false;
                return teacher;
            }

            return null;
        }

        private byte GetLessonCountDay(Lesson lesson)
        {
            byte count = 0;
            foreach (var scheduleLesson in _schedule)
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
                count = (byte)(Lesson.totalCountAWeek / 5 + 1);
                _classLessonsDiff[schoolClass]--;
            }
            else
                count = (byte)(Lesson.totalCountAWeek / 5);

            return count;
        }

        private byte GetComplexityForADay()
        {
            byte totalComplexity = 0;
            foreach (var scheduleLesson in _schedule)
            {
                totalComplexity += (byte)scheduleLesson.Item1.complexity;
            }

            return totalComplexity;
        }
    }
}
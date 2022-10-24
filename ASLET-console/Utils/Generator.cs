using System;
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
        private short _currentLessonsDiff;


        public Generator(List<Lesson> lessons, List<Teacher> teachers, List<Class> classes)
        {
            schedule = new List<Tuple<Lesson, Teacher>>();
            _lessons = lessons;
            _teachers = teachers;
            _classes = classes;
            _random = new Random();
        }

        public void GenerateForWeek()
        {
            if (!Checkers.CanTimetableBeGenerated(_lessons))
            {
                Console.WriteLine("Не може да бъде генерирана програма! ПРИЧИНА: НЯМА ДОСТАТЪЧНО СЕДМИЧНИ ЧАСОВЕ!");
                return;
            }

            foreach (Class schoolClass in _classes)
            {
                foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
                {
                    GenerateForDay(schoolClass, GetLessonsForADay());
                    Timetable.AddScheduleForDay(schoolClass, day, schedule);
                    schedule = new List<Tuple<Lesson, Teacher>>();
                    foreach (Teacher teacher in _teachers)
                    {
                        teacher.SetFreeLessons();
                    }
                }

                _currentLessonsDiff = 0;
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
                if (lessonsCount >= currentLesson.maxADay) continue;
                lessonsCount = GetLessonCountWeek(schoolClass, currentLesson);
                if (lessonsCount >= currentLesson.maxAWeek) continue;
                Teacher currentTeacher = GetFreeTeacher(currentLesson.subject, hour);
                if (currentTeacher != null)
                    return Tuple.Create(currentLesson, currentTeacher);
            } while (true);
        }

        private Teacher GetFreeTeacher(string subject, byte hour)
        {
            foreach (var teacher in _teachers)
            {
                if (teacher.subject != subject || !teacher.freeLessons[hour - 1]) continue;
                teacher.freeLessons[hour - 1] = false;
                return teacher;
            }

            return null!;
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

        private short findTotalHourDiff()
        {
            return (short)(Lesson.totalCount - 7 * 5);
        }

        private byte GetLessonsForADay()
        {
            byte count = 0;

            if (Lesson.totalCount == 7 * 5)
                count = 7;
            else if (Lesson.totalCount > 7 * 5)
            {
                if (_currentLessonsDiff < findTotalHourDiff())
                {
                    count = 8;
                    _currentLessonsDiff++;
                }
                else count = 7;
            }
            else if (Lesson.totalCount < 7 * 5)
            {
                if (_currentLessonsDiff > findTotalHourDiff())
                {
                    count = 6;
                    _currentLessonsDiff--;
                }
                else count = 7;
            }

            return count;
        }
    }
}
using System;
using ASLET.Objects;

namespace ASLET.Utils
{
    public class Generator
    {
        public List<Tuple<Lesson, Teacher>> schedule;
        private readonly List<Lesson> _lessons;
        private readonly List<Teacher> _teachers;
        private readonly Random _random;
        // private byte lessonsCount;

        public Generator(List<Lesson> lessons, List<Teacher> teachers)
        {
            schedule = new List<Tuple<Lesson, Teacher>>();
            _lessons = lessons;
            _teachers = teachers;
            _random = new Random();
            // lessonsCount = 0;
        }

        public void GenerateForWeek()
        {
            schedule = new List<Tuple<Lesson, Teacher>>();
            GenerateForDay();
            Timetable.AddScheduleForDay(DaysOfWeek.MONDAY, schedule);
            schedule = new List<Tuple<Lesson, Teacher>>();
            GenerateForDay();
            Timetable.AddScheduleForDay(DaysOfWeek.TUESDAY, schedule);
            schedule = new List<Tuple<Lesson, Teacher>>();
            GenerateForDay();
            Timetable.AddScheduleForDay(DaysOfWeek.WEDNESDAY, schedule);
            schedule = new List<Tuple<Lesson, Teacher>>();
            GenerateForDay();
            Timetable.AddScheduleForDay(DaysOfWeek.THURSDAY, schedule);
            schedule = new List<Tuple<Lesson, Teacher>>();
            GenerateForDay();
            Timetable.AddScheduleForDay(DaysOfWeek.FRIDAY, schedule);
            schedule = new List<Tuple<Lesson, Teacher>>();
            GenerateForDay();
            Timetable.AddScheduleForDay(DaysOfWeek.SATURDAY, schedule);
            schedule = new List<Tuple<Lesson, Teacher>>();
            GenerateForDay();
            Timetable.AddScheduleForDay(DaysOfWeek.SUNDAY, schedule);
            schedule = new List<Tuple<Lesson, Teacher>>();
        }

        public void GenerateForDay()
        {
            for (byte i = 1; i <= 8; i++){
                schedule.Add(GenerateNextLesson(i));
                // lessonsCount++;
            }
        }

        private Tuple<Lesson, Teacher> GenerateNextLesson(byte hour)
        {
            do {
                byte index = (byte) _random.Next(0, _lessons.Count);
                Lesson currentLesson = _lessons[index];
                byte lessonsCount = GetLessonCount(currentLesson);
                if (lessonsCount >= currentLesson.maxADay) continue;
                Teacher currentTeacher = GetFreeTeacher(currentLesson.subject, hour);
                if (currentTeacher != null)
                    return Tuple.Create(currentLesson, currentTeacher);
            } while(true);
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

        private byte GetLessonCount(Lesson lesson) {
            byte count = 0;
            foreach (var scheduleLesson in schedule)
            {
                if (scheduleLesson.Item1.subject == lesson.subject)
                    count++;
            }

            return count;
        }
    }
}
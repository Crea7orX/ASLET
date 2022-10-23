using System;
using ASLET.Objects;

namespace ASLET.Utils
{
    public class Generator
    {
        public List<Tuple<Lesson, Teacher>> schedule;
        private List<Lesson> lessons;
        private List<Teacher> teachers;
        private readonly Random random;
        // private byte lessonsCount;

        public Generator(List<Lesson> lessons, List<Teacher> teachers)
        {
            this.schedule = new List<Tuple<Lesson, Teacher>>();
            this.lessons = lessons;
            this.teachers = teachers;
            this.random = new Random();
            // lessonsCount = 0;
        }

        public void generateForDay()
        {
            for (byte i = 1; i <= 8; i++){
                this.schedule.Add(generateNextLesson(i));
                // lessonsCount++;
            }
        }

        private Tuple<Lesson, Teacher> generateNextLesson(byte hour)
        {
            do {
                byte index = (byte) random.Next(0, lessons.Count);
                Lesson currentLesson = lessons[index];
                byte lessonsCount = getLessonCount(currentLesson);
                if (lessonsCount >= currentLesson.maxADay) continue;
                Teacher currentTeacher = getFreeTeacher(currentLesson.subject, hour);
                if (currentTeacher != null)
                    return Tuple.Create(currentLesson, currentTeacher);
            } while(true);
        }

        private Teacher getFreeTeacher(string subject, byte hour)
        {
            for(int i = 0; i < teachers.Count; i++)
            {
                if (teachers[i].subject == subject && teachers[i].freeLessons[hour - 1])
                {
                    teachers[i].freeLessons[hour - 1] = false;
                    return teachers[i];
                }
            }
            return null!;
        }

        private byte getLessonCount(Lesson lesson) {
            byte count = 0;
            for(int i = 0; i < schedule.Count; i++){
                if (schedule[i].Item1.subject == lesson.subject)
                    count++;
            }

            return count;
        }
    }
}
namespace ASLET.Objects
{
    public enum Complexity
    {
        Easy = 1,
        Normal = 2,
        Hard = 3
    }

    public enum LessonType
    {
        Relaxing = 0,
        Narrative = 1,
        Language = 2,
        Algorithmic = 3,
        Science = 4,
        Sport = 5
    }

    public class Lesson
    {
        public static byte totalCount;
        public readonly string displayName;
        public readonly LessonType type;
        public readonly string subject;
        public readonly Complexity complexity;
        public readonly byte maxADay;
        public readonly byte maxAWeek;

        public Lesson(string displayName, LessonType type, string subject, Complexity complexity, byte maxADay, byte maxAWeek)
        {
            totalCount += maxAWeek;
            this.displayName = displayName;
            this.type = type;
            this.subject = subject;
            this.complexity = complexity;
            this.maxADay = maxADay;
            this.maxAWeek = maxAWeek;
        }
    }
}
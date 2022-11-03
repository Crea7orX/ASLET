namespace ASLET.Objects
{
    public enum Complexity
    {
        Nothing = 0,
        Easy = 1,
        Normal = 2,
        Hard = 3
    }

    public enum LessonType
    {
        Nothing = 0,
        Relaxing = 1,
        Narrative = 2,
        Language = 3,
        Algorithmic = 4,
        Science = 5,
        Sport = 6
    }

    public class Lesson
    {
        public static byte totalCountAWeek;
        public static byte totalComplexity;
        public readonly string displayName;
        public readonly LessonType type;
        public readonly string subject;
        public readonly Complexity complexity;
        public readonly byte maxADay;
        public readonly byte maxAWeek;

        public static Lesson nullLesson => new Lesson("ПРАЗНО", LessonType.Nothing, "nothing", Complexity.Nothing, 0, 0);

        public Lesson(string displayName, LessonType type, string subject, Complexity complexity, byte maxADay, byte maxAWeek)
        {
            totalCountAWeek += maxAWeek;
            totalComplexity += (byte)((byte)complexity * maxADay);
            this.displayName = displayName;
            this.type = type;
            this.subject = subject;
            this.complexity = complexity;
            this.maxADay = maxADay;
            this.maxAWeek = maxAWeek;
        }
    }
}
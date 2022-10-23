namespace ASLET.Objects
{
    public enum Complexity
    {
        EASY = 1,
        NORMAL = 2,
        HARD = 3
    }

    public enum LessonType
    {
        RELAXING = 0,
        NARRATIVE = 1,
        LANGUAGE = 2,
        ALGORITHMIC = 3,
        SCIENCE = 4
    }
    
    public class Lesson
    {
        public string displayName;
        public LessonType type;
        public string subject;
        public Complexity complexity;
        public byte maxADay;
        public byte maxAWeek;

        public Lesson(string displayName, LessonType type, string subject, Complexity complexity, byte maxADay, byte maxAWeek)
        {
            this.displayName = displayName;
            this.type = type;
            this.subject = subject;
            this.complexity = complexity;
            this.maxADay = maxADay;
            this.maxAWeek = maxAWeek;
        }
    }
}
namespace ASLET.Objects
{
    public class Teacher
    {
        public readonly string name;
        public readonly string subjects;
        public readonly bool[] freeLessons;
        public readonly List<Class> attachedClasses;

        public Teacher(string name, string subjects, List<Class> attachedClasses)
        {
            this.name = name;
            this.subjects = subjects;
            freeLessons = new bool[8];
            this.attachedClasses = attachedClasses;
            SetFreeLessons();
        }

        public void SetFreeLessons()
        {
            for (int i = 0; i < freeLessons.Length; i++)
            {
                freeLessons[i] = true;
            }
        }

        public bool TeachingSubject(string subject)
        {
            string[] subjectsArr = subjects.Split(",\\s+");
            return subjectsArr.Contains(subject);
        }
    }
}
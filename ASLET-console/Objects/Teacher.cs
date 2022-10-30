namespace ASLET.Objects
{
    public class Teacher
    {
        public readonly string name;
        public readonly string subject;
        public readonly bool[] freeLessons;
        public readonly List<Class> attachedClasses;

        public Teacher(string name, string subject, List<Class> attachedClasses)
        {
            this.name = name;
            this.subject = subject;
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
    }
}
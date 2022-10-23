namespace ASLET.Objects
{
    public class Teacher
    {
        public readonly string name;
        public readonly string subject;
        public readonly bool[] freeLessons;
        
        
        public Teacher(string name, string subject)
        {
            this.name = name;
            this.subject = subject;
            freeLessons = new bool[8];
            SetFreeLessons();
        }

        private void SetFreeLessons()
        {
            for(int i = 0; i < freeLessons.Length; i++){
                freeLessons[i] = true;
            }
        }
    }
}
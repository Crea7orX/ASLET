namespace ASLET.Objects
{
    public class Teacher
    {
        public string name;
        public string subject;
        public bool[] freeLessons;
        
        
        public Teacher(string name, string subject)
        {
            this.name = name;
            this.subject = subject;
            this.freeLessons = new bool[8];
            setFreeLessons();
        }

        private void setFreeLessons()
        {
            for(int i = 0; i < freeLessons.Length; i++){
                this.freeLessons[i] = true;
            }
        }
    }
}
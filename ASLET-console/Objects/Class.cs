using System.Text;

namespace ASLET.Objects
{
    public class Class
    {
        //    public readonly string className;
        //    public readonly Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>> timetable;
        //
        //    public Class(string className, Dictionary<DaysOfWeek, List<Tuple<Lesson, Teacher>>> timetalble)
        //    {
        //        this.className = className;
        //        this.timetable = timetalble;
        //    }
        public readonly byte classNumber;
        public readonly char classLetter;

        public string className
        {
            get
            {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(classNumber);
                    sb.Append(classLetter);
                    return sb.ToString();
            }
        }

        public Class(byte classNumber, char classLetter)
        {
            this.classNumber = classNumber;
            this.classLetter = classLetter;
        }
    }
}
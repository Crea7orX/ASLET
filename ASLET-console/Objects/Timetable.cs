using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ASLET.Objects;

namespace ASLET.Objects
{
    public enum DaysOfWeek{
        MONDAY = 1,
        TUESDAY = 2,
        WEDNESDAY = 3,
        THURSDAY = 4,
        FRIDAY = 5,
        SATURDAY = 6,
        SUNDAY = 7
    }
    
    public class Timetable
    {
        public static Dictionary<DaysOfWeek, List<Lesson>> timetable = new Dictionary<DaysOfWeek, List<Lesson>>();

        public static void addScheduleForDay(DaysOfWeek day, List<Lesson> lessons) {
            timetable.Add(day, lessons);
        }
    }
}
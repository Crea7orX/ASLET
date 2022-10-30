using ASLET.Objects;

namespace ASLET.Utils;

public class Debug
{
    public static void SaveTimetable(List<Class> classes)
    {
        if (!File.Exists("./timetable.txt")) File.Create("./timetable.txt");
        using StreamWriter writer = new StreamWriter("./timetable.txt");
        foreach (Class schoolClass in classes)
        {
            writer.WriteLine(schoolClass.className);
            writer.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                foreach (List<Tuple<Lesson, Teacher>> currentDay in Timetable.timetable[schoolClass].Values)
                {
                    if (currentDay.Count - 1 < i) continue;
                    if (currentDay[i].Item1 == null) continue;
                    writer.Write(currentDay[i].Item1.displayName.PadRight(40));
                }

                writer.WriteLine();
            }
            writer.WriteLine();
            foreach (List<Tuple<Lesson, Teacher>> currentDay in Timetable.timetable[schoolClass].Values)
            {
                int complexity = 0;
                foreach (Tuple<Lesson, Teacher> currentLesson in currentDay)
                {
                    if (currentLesson.Item1 == null) continue;
                    complexity += (int)currentLesson.Item1.complexity;
                }

                writer.Write(("Complexity " + complexity).PadRight(40));
            }

            for (int i = 0; i < 3; i++) writer.WriteLine();
        }

        writer.Close();
    }
}
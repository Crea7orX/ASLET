﻿using ASLET.Objects;

namespace ASLET.Utils;

public class Debug
{
    const string outputFile = "./timetable.txt";

    public static void SaveTimetable(List<Class> classes)
    {
        if (!File.Exists(outputFile)) File.Create(outputFile);
        StreamWriter writer = new StreamWriter(outputFile);
        foreach (Class schoolClass in classes)
        {
            writer.WriteLine(schoolClass.className);
            writer.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                foreach (List<Tuple<Lesson, Teacher>> currentDay in Timetable.timetable[schoolClass].Values)
                {
                    if (currentDay.Count - 1 < i) continue;
                    writer.Write(currentDay[i].Item1.displayName.ToUpper().PadRight(40));
                }

                writer.WriteLine();
                
                foreach (List<Tuple<Lesson, Teacher>> currentDay in Timetable.timetable[schoolClass].Values)
                {
                    if (currentDay.Count - 1 < i) continue;
                    writer.Write(currentDay[i].Item2.name.PadRight(40));
                }

                writer.WriteLine();
            }
            writer.WriteLine();
            foreach (List<Tuple<Lesson, Teacher>> currentDay in Timetable.timetable[schoolClass].Values)
            {
                int complexity = 0;
                foreach (Tuple<Lesson, Teacher> currentLesson in currentDay)
                {
                    complexity += (int)currentLesson.Item1.complexity;
                }

                writer.Write(("Complexity " + complexity).PadRight(40));
            }

            for (int i = 0; i < 3; i++) writer.WriteLine();
        }

        writer.Close();
    }
}
using ASLET.Objects;
using ASLET.Utils;

namespace ASLET
{
    static class Program
    {
        private static readonly List<Lesson> Lessons = new();
        private static readonly List<Teacher> Teachers = new();
        private static readonly List<Class> Classes = new();

        public static void Main(string[] args)
        {
            FillClasses();
            FillLessons();
            FillTeachers();
            Generator generator = new Generator(Lessons, Teachers, Classes);

            generator.GenerateForWeek();

            //$ FIRST DEBUG OUTPUT
            //Console.WriteLine("Hello? SiR");
            /* foreach (Tuple<Lesson, Teacher> currentLesson in generator.schedule)
            {
                Console.Write(currentLesson.Item1.displayName.PadRight(60));
                Console.Write(currentLesson.Item2.name.PadRight(30));
                Console.Write(String.Join(", ", currentLesson.Item2.freeLessons).PadRight(60));
                int freeLessonsCount = 0;
                foreach (bool lesson in currentLesson.Item2.freeLessons)
                    if (lesson) freeLessonsCount++;
                Console.WriteLine(freeLessonsCount);
            } */
            Console.WriteLine("FINISHED!");
            Debug.SaveTimetable(Classes);
            // foreach (Class schoolClass in Classes)
            // {
            //     Console.WriteLine(schoolClass.className);
            //     foreach (List<Tuple<Lesson, Teacher>> currentDay in Timetable.timetable[schoolClass].Values)
            //     {
            //         Console.WriteLine();
            //         // Console.WriteLine(
            //         //     "--------------------------------------------------------------------------------------------------------------------------------------------");
            //         foreach (Tuple<Lesson, Teacher> currentLesson in currentDay)
            //         {
            //             Console.Write(currentLesson.Item1.displayName.PadRight(60));
            //             Console.WriteLine();
            //             // Console.Write(currentLesson.Item2.name.PadRight(30));
            //             // Console.Write(String.Join(", ", currentLesson.Item2.freeLessons).PadRight(60));
            //             // int freeLessonsCount = 0;
            //             // foreach (bool lesson in currentLesson.Item2.freeLessons)
            //             //     if (lesson)
            //             //         freeLessonsCount++;
            //             // Console.WriteLine(freeLessonsCount);
            //         }
            //
            //         // Console.WriteLine(
            //         //     "--------------------------------------------------------------------------------------------------------------------------------------------");
            //     }
            // }
        }

        private static void FillLessons()
        {
            Lessons.Add(new Lesson("Български език и литература", LessonType.Language, "bulgarian", Complexity.Normal,
                2, 4));
            Lessons.Add(new Lesson("Математика", LessonType.Algorithmic, "math", Complexity.Hard, 2, 4));
            Lessons.Add(new Lesson("Философия", LessonType.Narrative, "philosophy", Complexity.Normal, 1, 2));
            Lessons.Add(new Lesson("География и икономика", LessonType.Narrative, "geography", Complexity.Normal, 1,
                2));
            Lessons.Add(new Lesson("Физика и астрономия", LessonType.Science, "physics", Complexity.Hard, 1, 2));
            Lessons.Add(new Lesson("Биология и здравно образование", LessonType.Science, "biology", Complexity.Hard, 1,
                2));
            Lessons.Add(new Lesson("Химия и опазване на околната среда", LessonType.Science, "chemistry",
                Complexity.Hard, 1, 2));
            Lessons.Add(new Lesson("Музика", LessonType.Relaxing, "music", Complexity.Easy, 0, 0));
            Lessons.Add(new Lesson("Изобразително изкуство", LessonType.Relaxing, "art", Complexity.Easy, 1, 1));
            Lessons.Add(new Lesson("Информационни технологии", LessonType.Algorithmic, "it", Complexity.Easy, 0, 0));
            Lessons.Add(new Lesson("Физическо възпитание и спорт", LessonType.Sport, "pe", Complexity.Easy, 1,
                2));
            Lessons.Add(new Lesson("Английски език", LessonType.Language, "english", Complexity.Normal, 2, 2));
            Lessons.Add(new Lesson("Немски език", LessonType.Language, "german", Complexity.Normal, 2, 2));
            Lessons.Add(new Lesson("Час на класа", LessonType.Relaxing, "classhour", Complexity.Easy, 1, 1));
            Lessons.Add(new Lesson("История и цивилизации", LessonType.Narrative, "history", Complexity.Normal, 1, 4));
            Lessons.Add(new Lesson("ПРАЗНО", LessonType.Nothing, "nothing", Complexity.Nothing, 0, 0));
        }

        private static void FillTeachers()
        {
            List<Class> VIIIA = new List<Class> { Classes[0] };
            List<Class> VIIIB = new List<Class> { Classes[1] };
            List<Class> BOTH = new List<Class> { Classes[0], Classes[1] };
            Teachers.Add(new Teacher("Лилия Й. Колева", "bulgarian", VIIIA));
            Teachers.Add(new Teacher("Павлина Я. Коларова", "math", VIIIA));
            Teachers.Add(new Teacher("Ивелина Ю. Михайлова", "philosophy", VIIIA));
            Teachers.Add(new Teacher("Зорница Й. Атанасова", "geography", VIIIA));
            Teachers.Add(new Teacher("Диана Х. Димова", "physics", VIIIA));
            Teachers.Add(new Teacher("Павлина Й. Няголова", "biology", VIIIA));
            Teachers.Add(new Teacher("Жечка С. Владимирова", "chemistry", VIIIA));
            Teachers.Add(new Teacher("Радостин К. Златинов", "music", VIIIA));
            Teachers.Add(new Teacher("Кирил С. Стефанов ", "art", VIIIA));
            Teachers.Add(new Teacher("Полина Кирилова", "it", VIIIA));
            Teachers.Add(new Teacher("Елена Дончева", "pe", VIIIA));
            Teachers.Add(new Teacher("Ивелина Ю. Михайлова", "english", VIIIA));
            Teachers.Add(new Teacher("Веселина Янкова", "german", VIIIA));
            Teachers.Add(new Teacher("Павлина Й. Няголова", "classhour", VIIIA));
            Teachers.Add(new Teacher("Кирил К. Димитров", "history", VIIIA));
            
            Teachers.Add(new Teacher("Лилия Й. Колева2", "bulgarian", VIIIB));
            Teachers.Add(new Teacher("Павлина Я. Коларова2", "math", VIIIB));
            Teachers.Add(new Teacher("Ивелина Ю. Михайлова2", "philosophy", VIIIB));
            Teachers.Add(new Teacher("Зорница Й. Атанасова2", "geography", VIIIB));
            Teachers.Add(new Teacher("Диана Х. Димова2", "physics", VIIIB));
            Teachers.Add(new Teacher("Павлина Й. Няголова2", "biology", BOTH));
            Teachers.Add(new Teacher("Жечка С. Владимирова2", "chemistry", VIIIB));
            Teachers.Add(new Teacher("Радостин К. Златинов2", "music", VIIIB));
            Teachers.Add(new Teacher("Кирил С. Стефанов 2", "art", VIIIB));
            Teachers.Add(new Teacher("Полина Кирилова2", "it", VIIIB));
            Teachers.Add(new Teacher("Елена Дончева2", "pe", VIIIB));
            Teachers.Add(new Teacher("Ивелина Ю. Михайлова2", "english", VIIIB));
            Teachers.Add(new Teacher("Веселина Янкова2", "german", VIIIB));
            Teachers.Add(new Teacher("Павлина Й. Няголова2", "classhour", VIIIB));
            Teachers.Add(new Teacher("Кирил К. Димитров2", "history", VIIIB));
        }

        private static void FillClasses()
        {
            Classes.Add(new Class("8А"));
            Classes.Add(new Class("8Б"));
        }
    }
}
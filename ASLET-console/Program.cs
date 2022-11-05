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
            Setup.SetupProgram();
            return;
            
            FillClasses();
            FillLessons();
            FillTeachers();
            Generator generator = new Generator(Lessons, Teachers, Classes);

            generator.GenerateForWeek();

            //$ FIRST DEBUG OUTPUT
            //Console.WriteLine("Hello? SiR");
            Console.WriteLine("FINISHED!");
            Debug.SaveTimetable(Classes);
        }

        private static void FillLessons()
        {
            Lessons.Add(new Lesson("Български език и литература", LessonType.Language, "bulgarian", Complexity.Normal, 2, 4));
            Lessons.Add(new Lesson("Математика", LessonType.Algorithmic, "math", Complexity.Hard, 2, 4));
            Lessons.Add(new Lesson("Философия", LessonType.Narrative, "philosophy", Complexity.Normal, 1, 2));
            Lessons.Add(new Lesson("География и икономика", LessonType.Narrative, "geography", Complexity.Normal, 1, 2));
            Lessons.Add(new Lesson("Физика и астрономия", LessonType.Science, "physics", Complexity.Hard, 1, 2));
            Lessons.Add(new Lesson("Биология и здравно образование", LessonType.Science, "biology", Complexity.Hard, 1, 2));
            Lessons.Add(new Lesson("Химия и опазване на околната среда", LessonType.Science, "chemistry", Complexity.Hard, 1, 2));
            Lessons.Add(new Lesson("Музика", LessonType.Relaxing, "music", Complexity.Easy, 0, 0));
            Lessons.Add(new Lesson("Изобразително изкуство", LessonType.Relaxing, "art", Complexity.Easy, 1, 1));
            Lessons.Add(new Lesson("Информационни технологии", LessonType.Algorithmic, "it", Complexity.Easy, 0, 0));
            Lessons.Add(new Lesson("Физическо възпитание и спорт", LessonType.Sport, "pe", Complexity.Easy, 1, 2));
            Lessons.Add(new Lesson("Английски език", LessonType.Language, "english", Complexity.Normal, 2, 2));
            Lessons.Add(new Lesson("Немски език", LessonType.Language, "german", Complexity.Normal, 2, 2));
            Lessons.Add(new Lesson("Час на класа", LessonType.Relaxing, "classhour", Complexity.Easy, 1, 1));
            Lessons.Add(new Lesson("История и цивилизации", LessonType.Narrative, "history", Complexity.Normal, 1, 4));
        }

        private static void FillTeachers()
        {
            List<Class> VIIIA = new List<Class> { Classes[0] };
            List<Class> VIIIB = new List<Class> { Classes[1] };
            List<Class> BOTH = new List<Class> { Classes[0], Classes[1] };
            Teachers.Add(new Teacher("Лилия Й. Колева", "bulgarian", BOTH));
            Teachers.Add(new Teacher("Павлина Я. Коларова", "math", BOTH));
            Teachers.Add(new Teacher("Ивелина Ю. Михайлова", "philosophy", BOTH));
            Teachers.Add(new Teacher("Зорница Й. Атанасова", "geography", BOTH));
            Teachers.Add(new Teacher("Диана Х. Димова", "physics", BOTH));
            Teachers.Add(new Teacher("Павлина Й. Няголова", "biology, classhour", BOTH));
            Teachers.Add(new Teacher("Жечка С. Владимирова", "chemistry", BOTH));
            Teachers.Add(new Teacher("Радостин К. Златинов", "music", BOTH));
            Teachers.Add(new Teacher("Кирил С. Стефанов ", "art", BOTH));
            Teachers.Add(new Teacher("Полина Кирилова", "it", BOTH));
            Teachers.Add(new Teacher("Елена Дончева", "pe", BOTH));
            Teachers.Add(new Teacher("Ивелина Ю. Михайлова", "english", BOTH));
            Teachers.Add(new Teacher("Веселина Янкова", "german", BOTH));
            Teachers.Add(new Teacher("Кирил К. Димитров", "history", BOTH));

            // Teachers.Add(new Teacher("Лилия Й. Колева2", "bulgarian", BOTH));
            // Teachers.Add(new Teacher("Павлина Я. Коларова2", "math", BOTH));
            // Teachers.Add(new Teacher("Ивелина Ю. Михайлова2", "philosophy", BOTH));
            // Teachers.Add(new Teacher("Зорница Й. Атанасова2", "geography", BOTH));
            // Teachers.Add(new Teacher("Диана Х. Димова2", "physics", BOTH));
            // Teachers.Add(new Teacher("Павлина Й. Няголова2", "biology", BOTH));
            // Teachers.Add(new Teacher("Жечка С. Владимирова2", "chemistry", BOTH));
            // Teachers.Add(new Teacher("Радостин К. Златинов2", "music", BOTH));
            // Teachers.Add(new Teacher("Кирил С. Стефанов 2", "art", BOTH));
            // Teachers.Add(new Teacher("Полина Кирилова2", "it", BOTH));
            // Teachers.Add(new Teacher("Елена Дончева2", "pe", BOTH));
            // Teachers.Add(new Teacher("Ивелина Ю. Михайлова2", "english", BOTH));
            // Teachers.Add(new Teacher("Веселина Янкова2", "german", BOTH));
            // Teachers.Add(new Teacher("Павлина Й. Няголова2", "classhour", BOTH));
            // Teachers.Add(new Teacher("Кирил К. Димитров2", "history", BOTH));
        }

        private static void FillClasses()
        {
            Classes.Add(new Class(8, 'А'));
            Classes.Add(new Class(8, 'Б'));
        }
    }
}
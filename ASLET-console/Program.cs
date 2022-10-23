using ASLET.Objects;
using ASLET.Utils;

namespace ASLET
{
    static class Program
    {
        private static readonly List<Lesson> _lessons = new();
        private static readonly List<Teacher> _teachers = new();

        public static void Main(string[] args)
        {
            FillLessons();
            FillTeachers();
            Generator generator = new Generator(_lessons, _teachers);

            generator.GenerateForDay();

            //$ FIRST DEBUG OUTPUT
            //Console.WriteLine("Hello? SiR");
            foreach (Tuple<Lesson, Teacher> currentLesson in generator.schedule){
                Console.WriteLine("{0} {1}",currentLesson.Item1.displayName, currentLesson.Item2.name);
            }
        }

        private static void FillLessons() {
            _lessons.Add(new Lesson("Български език и литература", LessonType.LANGUAGE, "bulgarian", Complexity.NORMAL, 1, 1));
            _lessons.Add(new Lesson("Математика", LessonType.ALGORITHMIC, "math", Complexity.HARD, 1, 1));
            _lessons.Add(new Lesson("Философия", LessonType.NARRATIVE, "philosophy", Complexity.NORMAL, 1, 1));
            _lessons.Add(new Lesson("География и икономика", LessonType.NARRATIVE, "geography", Complexity.NORMAL, 1, 1));
            _lessons.Add(new Lesson("Физика и астрономия", LessonType.SCIENCE, "physics", Complexity.HARD, 1, 1));
            _lessons.Add(new Lesson("Биология и здравно образование", LessonType.SCIENCE, "biology", Complexity.HARD, 1, 1));
            _lessons.Add(new Lesson("Химия и опазване на околната среда", LessonType.SCIENCE, "chemistry", Complexity.HARD, 1, 1));
            _lessons.Add(new Lesson("Музика", LessonType.RELAXING, "music", Complexity.EASY, 1, 1));
            _lessons.Add(new Lesson("Изобразително изкуство", LessonType.ALGORITHMIC, "art", Complexity.EASY, 1, 1));
            _lessons.Add(new Lesson("Информационни технологии", LessonType.ALGORITHMIC, "it", Complexity.EASY, 1, 1));
            _lessons.Add(new Lesson("Физическо възпитание и спорт", LessonType.ALGORITHMIC, "pe", Complexity.EASY, 1, 1));
            _lessons.Add(new Lesson("Английски език", LessonType.ALGORITHMIC, "english", Complexity.NORMAL, 1, 1));
            _lessons.Add(new Lesson("Немски език", LessonType.ALGORITHMIC, "german", Complexity.NORMAL, 1, 1));
            _lessons.Add(new Lesson("Час на класа", LessonType.ALGORITHMIC, "classhour", Complexity.EASY, 1, 1));
            _lessons.Add(new Lesson("История и цивилизации", LessonType.NARRATIVE, "history", Complexity.NORMAL, 1, 1));
        }
        private static void FillTeachers(){
            _teachers.Add(new Teacher("Лилия Й. Колева", "bulgarian"));
            _teachers.Add(new Teacher("Павлина Я. Коларова", "math"));
            _teachers.Add(new Teacher("Ивелина Ю. Михайлова", "philosophy"));
            _teachers.Add(new Teacher("Зорница Й. Атанасова", "geography"));
            _teachers.Add(new Teacher("Диана Х. Димова", "physics"));
            _teachers.Add(new Teacher("Павлина Й. Няголова", "biology"));
            _teachers.Add(new Teacher("Жечка С. Владимирова", "chemistry"));
            _teachers.Add(new Teacher("Радостин К. Златинов", "music"));
            _teachers.Add(new Teacher("Кирил С. Стефанов ", "art"));
            _teachers.Add(new Teacher("Полина Кирилова", "it"));
            _teachers.Add(new Teacher("Елена Дончева", "pe"));
            _teachers.Add(new Teacher("Ивелина Ю. Михайлова", "english"));
            _teachers.Add(new Teacher("Веселина Янкова", "german"));
            _teachers.Add(new Teacher("Павлина Й. Няголова", "classhour"));
            _teachers.Add(new Teacher("Кирил К. Димитров", "history"));
        }
    }
}
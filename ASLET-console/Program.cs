using System;
using System.Collections.Generic;
using ASLET.Objects;
using ASLET.Utils;

namespace ASLET
{
    class program
    {
        private static List<Lesson> lessons = new List<Lesson>();
        private static List<Teacher> teachers = new List<Teacher>();

        public static void Main(string[] args)
        {
            fillLessons();
            fillTeachers();
            Generator generator = new Generator(lessons, teachers);

            generator.generateForDay();

            //$ FIRST DEBUG OUTPUT
            //Console.WriteLine("Hello? SiR");
            foreach (Tuple<Lesson, Teacher> currentLesson in generator.schedule){
                Console.WriteLine("{0} {1}",currentLesson.Item1.displayName, currentLesson.Item2.name);
            }
        }

        private static void fillLessons() {
            lessons.Add(new Lesson("Български език и литература", LessonType.LANGUAGE, "bulgarian", Complexity.NORMAL, 1, 1));
            lessons.Add(new Lesson("Математика", LessonType.ALGORITHMIC, "math", Complexity.HARD, 1, 1));
            lessons.Add(new Lesson("Философия", LessonType.NARRATIVE, "philosophy", Complexity.NORMAL, 1, 1));
            lessons.Add(new Lesson("География и икономика", LessonType.NARRATIVE, "geography", Complexity.NORMAL, 1, 1));
            lessons.Add(new Lesson("Физика и астрономия", LessonType.SCIENCE, "physics", Complexity.HARD, 1, 1));
            lessons.Add(new Lesson("Биология и здравно образование", LessonType.SCIENCE, "biology", Complexity.HARD, 1, 1));
            lessons.Add(new Lesson("Химия и опазване на околната среда", LessonType.SCIENCE, "chemistry", Complexity.HARD, 1, 1));
            lessons.Add(new Lesson("Музика", LessonType.RELAXING, "music", Complexity.EASY, 1, 1));
            lessons.Add(new Lesson("Изобразително изкуство", LessonType.ALGORITHMIC, "art", Complexity.EASY, 1, 1));
            lessons.Add(new Lesson("Информационни технологии", LessonType.ALGORITHMIC, "it", Complexity.EASY, 1, 1));
            lessons.Add(new Lesson("Физическо възпитание и спорт", LessonType.ALGORITHMIC, "pe", Complexity.EASY, 1, 1));
            lessons.Add(new Lesson("Английски език", LessonType.ALGORITHMIC, "english", Complexity.NORMAL, 1, 1));
            lessons.Add(new Lesson("Немски език", LessonType.ALGORITHMIC, "german", Complexity.NORMAL, 1, 1));
            lessons.Add(new Lesson("Час на класа", LessonType.ALGORITHMIC, "classhour", Complexity.EASY, 1, 1));
            lessons.Add(new Lesson("История и цивилизации", LessonType.NARRATIVE, "history", Complexity.NORMAL, 1, 1));
        }
        private static void fillTeachers(){
            teachers.Add(new Teacher("Лилия Й. Колева", "bulgarian"));
            teachers.Add(new Teacher("Павлина Я. Коларова", "math"));
            teachers.Add(new Teacher("Ивелина Ю. Михайлова", "philosophy"));
            teachers.Add(new Teacher("Зорница Й. Атанасова", "geography"));
            teachers.Add(new Teacher("Диана Х. Димова", "physics"));
            teachers.Add(new Teacher("Павлина Й. Няголова", "biology"));
            teachers.Add(new Teacher("Жечка С. Владимирова", "chemistry"));
            teachers.Add(new Teacher("Радостин К. Златинов", "music"));
            teachers.Add(new Teacher("Кирил С. Стефанов ", "art"));
            teachers.Add(new Teacher("Полина Кирилова", "it"));
            teachers.Add(new Teacher("Елена Дончева", "pe"));
            teachers.Add(new Teacher("Ивелина Ю. Михайлова", "english"));
            teachers.Add(new Teacher("Веселина Янкова", "german"));
            teachers.Add(new Teacher("Павлина Й. Няголова", "classhour"));
            teachers.Add(new Teacher("Кирил К. Димитров", "history"));
        }
    }
}
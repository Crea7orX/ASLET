using ASLET.Handlers;
using ASLET.Objects;
using ASLET.Utils;
using Class = ASLET.Handlers.Class;

namespace ASLET;

public static class Setup
{
    public static void SetupProgram()
    {
        Console.WriteLine("STARTING SETUP");

        Console.WriteLine("SETUP CLASSES");
        GlobalSpace.ClassController.Add("8А");
        GlobalSpace.ClassController.Add("8Б");
        GlobalSpace.ClassController.Add("8В");
        GlobalSpace.ClassController.Add("8Г");
        GlobalSpace.ClassController.Add("8Д");
        Console.WriteLine("END SETUP CLASSES");

        Console.WriteLine("SETUP SUBJECTS");
        GlobalSpace.SubjectController.Add("Български-език-и-литература");
        GlobalSpace.SubjectController.Add("Математика");
        GlobalSpace.SubjectController.Add("Философия");
        GlobalSpace.SubjectController.Add("География-и-икономика");
        GlobalSpace.SubjectController.Add("Физика-и-астрономия");
        GlobalSpace.SubjectController.Add("Биология-и-здравно-образование");
        GlobalSpace.SubjectController.Add("Химия-и-опазване-на-околната-среда");
        GlobalSpace.SubjectController.Add("Музика");
        GlobalSpace.SubjectController.Add("Изобразително-изкуство");
        GlobalSpace.SubjectController.Add("Информационни-технологии");
        GlobalSpace.SubjectController.Add("Физическо-възпитание-и-спорт");
        GlobalSpace.SubjectController.Add("Английски-език");
        GlobalSpace.SubjectController.Add("Немски-език");
        GlobalSpace.SubjectController.Add("Час-на-класа");
        GlobalSpace.SubjectController.Add("История-и-цивилизации");
        Console.WriteLine("END SETUP SUBJECTS");
        
        Console.WriteLine("SETUP TEACHERS");
        GlobalSpace.TeacherController.Add("Лилия-Й.-Колева");
        GlobalSpace.TeacherController.Add("Павлина-Я.-Коларова");
        GlobalSpace.TeacherController.Add("Ивелина-Ю.-Михайлова");
        GlobalSpace.TeacherController.Add("Зорница-Й.-Атанасова");
        GlobalSpace.TeacherController.Add("Диана-Х.-Димова");
        GlobalSpace.TeacherController.Add("Павлина-Й.-Няголова");
        GlobalSpace.TeacherController.Add("Жечка-С.-Владимирова");
        GlobalSpace.TeacherController.Add("Радостин-К.-Златинов");
        GlobalSpace.TeacherController.Add("Кирил-С.-Стефанов");
        GlobalSpace.TeacherController.Add("Полина-Кирилова");
        GlobalSpace.TeacherController.Add("Елена-Дончева");
        GlobalSpace.TeacherController.Add("Ивелина-Ю.-Михайлова");
        GlobalSpace.TeacherController.Add("Веселина-Янкова");
        GlobalSpace.TeacherController.Add("Кирил-К.-Димитров");
        Console.WriteLine("END SETUP TEACHERS");

        Console.WriteLine("SETUP ASSIGNMENTS");
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Български-език-и-литература", "Лилия-Й.-Колева"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Математика", "Павлина-Я.-Коларова"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Философия", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("География-и-икономика", "Зорница-Й.-Атанасова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Физика-и-астрономия", "Диана-Х.-Димова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Биология-и-здравно-образование", "Павлина-Й.-Няголова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Химия-и-опазване-на-околната-среда", "Жечка-С.-Владимирова"), 2);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8А"].subjectPlan, new SubjectExample("Музика", "Радостин-К.-Златинов"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Изобразително-изкуство", "Кирил-С.-Стефанов"), 1);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8А"].subjectPlan, new SubjectExample("Информационни-технологии", "Полина-Кирилова"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Физическо-възпитание-и-спорт", "Елена-Дончева"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Английски-език", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Немски-език", "Веселина-Янкова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("Час-на-класа", "Павлина-Й.-Няголова"), 1);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8А"].SubjectPlan, new SubjectExample("История-и-цивилизации", "Кирил-К.-Димитров"), 4);
        
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Български-език-и-литература", "Лилия-Й.-Колева"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Математика", "Павлина-Я.-Коларова"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Философия", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("География-и-икономика", "Зорница-Й.-Атанасова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Физика-и-астрономия", "Диана-Х.-Димова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Биология-и-здравно-образование", "Павлина-Й.-Няголова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Химия-и-опазване-на-околната-среда", "Жечка-С.-Владимирова"), 2);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8Б"].subjectPlan, new SubjectExample("Музика", "Радостин-К.-Златинов"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Изобразително-изкуство", "Кирил-С.-Стефанов"), 1);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8Б"].subjectPlan, new SubjectExample("Информационни-технологии", "Полина-Кирилова"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Физическо-възпитание-и-спорт", "Елена-Дончева"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Английски-език", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Немски-език", "Веселина-Янкова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("Час-на-класа", "Павлина-Й.-Няголова"), 1);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Б"].SubjectPlan, new SubjectExample("История-и-цивилизации", "Кирил-К.-Димитров"), 4);
        
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Български-език-и-литература", "Лилия-Й.-Колева"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Математика", "Павлина-Я.-Коларова"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Философия", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("География-и-икономика", "Зорница-Й.-Атанасова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Физика-и-астрономия", "Диана-Х.-Димова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Биология-и-здравно-образование", "Павлина-Й.-Няголова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Химия-и-опазване-на-околната-среда", "Жечка-С.-Владимирова"), 2);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8В"].subjectPlan, new SubjectExample("Музика", "Радостин-К.-Златинов"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Изобразително-изкуство", "Кирил-С.-Стефанов"), 1);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8В"].subjectPlan, new SubjectExample("Информационни-технологии", "Полина-Кирилова"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Физическо-възпитание-и-спорт", "Елена-Дончева"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Английски-език", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Немски-език", "Веселина-Янкова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("Час-на-класа", "Павлина-Й.-Няголова"), 1);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8В"].SubjectPlan, new SubjectExample("История-и-цивилизации", "Кирил-К.-Димитров"), 4);
        
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Български-език-и-литература", "Лилия-Й.-Колева"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Математика", "Павлина-Я.-Коларова"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Философия", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("География-и-икономика", "Зорница-Й.-Атанасова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Физика-и-астрономия", "Диана-Х.-Димова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Биология-и-здравно-образование", "Павлина-Й.-Няголова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Химия-и-опазване-на-околната-среда", "Жечка-С.-Владимирова"), 2);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8Г"].subjectPlan, new SubjectExample("Музика", "Радостин-К.-Златинов"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Изобразително-изкуство", "Кирил-С.-Стефанов"), 1);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8Г"].subjectPlan, new SubjectExample("Информационни-технологии", "Полина-Кирилова"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Физическо-възпитание-и-спорт", "Елена-Дончева"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Английски-език", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Немски-език", "Веселина-Янкова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("Час-на-класа", "Павлина-Й.-Няголова"), 1);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Г"].SubjectPlan, new SubjectExample("История-и-цивилизации", "Кирил-К.-Димитров"), 4);
        
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Български-език-и-литература", "Лилия-Й.-Колева"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Математика", "Павлина-Я.-Коларова"), 4);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Философия", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("География-и-икономика", "Зорница-Й.-Атанасова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Физика-и-астрономия", "Диана-Х.-Димова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Биология-и-здравно-образование", "Павлина-Й.-Няголова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Химия-и-опазване-на-околната-среда", "Жечка-С.-Владимирова"), 2);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8Д"].subjectPlan, new SubjectExample("Музика", "Радостин-К.-Златинов"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Изобразително-изкуство", "Кирил-С.-Стефанов"), 1);
        // DictionaryUtils.Put(GlobalSpace.classController.classes["8Д"].subjectPlan, new SubjectExample("Информационни-технологии", "Полина-Кирилова"), 0);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Физическо-възпитание-и-спорт", "Елена-Дончева"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Английски-език", "Ивелина-Ю.-Михайлова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Немски-език", "Веселина-Янкова"), 2);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("Час-на-класа", "Павлина-Й.-Няголова"), 1);
        DictionaryUtils.Put(GlobalSpace.ClassController.Classes["8Д"].SubjectPlan, new SubjectExample("История-и-цивилизации", "Кирил-К.-Димитров"), 4);
        Console.WriteLine("END SETUP ASSIGNMENTS");

        Console.WriteLine("START GENERATING");
        Thread thread = new Thread(GlobalSpace.MakePlan);
        thread.Start();
        thread.Join();
        Console.ReadLine();
        GlobalSpace.UpdateClassController();
        Console.WriteLine("END GENERATING");
        Console.WriteLine(GlobalSpace.ready);
        // foreach (Class schoolClass in GlobalSpace.ClassController.Classes.Values)
        // {
        //     Console.WriteLine(schoolClass.Name);
        //     for (int i = 0; i < 5; i++)
        //     {
        //         Console.WriteLine(i);
        //         for (int j = 0; j < 7; j++)
        //         {
        //             Console.WriteLine(j);
        //             SubjectExample subjectExample = schoolClass.Schedule[i, j];
        //             Console.WriteLine(subjectExample.Subject + " " + subjectExample.Teacher);
        //         }
        //     }
        //     // foreach (SubjectExample subjectExample in schoolClass.schedule)
        //     // {
        //     //     Console.WriteLine(subjectExample.subject + " " + subjectExample.teacher);
        //     // }
        // }
    }
}
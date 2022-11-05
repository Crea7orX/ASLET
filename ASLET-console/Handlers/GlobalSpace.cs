using ASLET.Utils;

namespace ASLET.Handlers;

public static class GlobalSpace
{
    public static readonly ClassHandler ClassController;
    public static readonly TeacherHandler TeacherController;
    public static readonly SubjectHandler SubjectController;
    private static readonly Dictionary<object, object> Parameters;
    public static bool ready = false;

    static GlobalSpace()
    {
        ClassController = new ClassHandler();
        TeacherController = new TeacherHandler();
        SubjectController = new SubjectHandler();
        Parameters = new Dictionary<object, object>();
    }

    // TODO PARAMETERS
    public static void SetParameter(Object parameter, Object value)
    {
        DictionaryUtils.Put(Parameters, parameter, value);
    }

    public static void AddParameter(Object parameter)
    {
        if (!Parameters.ContainsKey(parameter))
        {
            DictionaryUtils.Put(Parameters, parameter, "");
        }
    }

    public static void MakePlan()
    {
        ScheduleFabric.algControll = new Controller();
        Dictionary<string, List<Class>> classDictionary = new Dictionary<string, List<Class>>();
        foreach (Class schoolClass in ClassController.Classes.Values)
        {
            string name = schoolClass.Name;
            string grade = null;
            if (name.Length == 2)
            {
                grade = name.Substring(0, 1);
            }
            else if (name.Length == 3)
            {
                grade = name.Substring(0, 2);
            }

            if (grade == null)
            {
                Console.WriteLine("Tou have too many classes!");
            }

            if (!classDictionary.ContainsKey(grade))
            {
                DictionaryUtils.Put(classDictionary, grade, new List<Class>());
            }

            classDictionary[grade].Add(schoolClass);
        }

        foreach (List<Class> classes in classDictionary.Values)
        {
            ClassController.SetUpController(ScheduleFabric.algControll, new List<Class>(classes));
            ScheduleFabric.algControll.Make();
        }

        UpdateClassController();
        RunMistakeChecker();
        ready = true;
    }

    private static void RunMistakeChecker()
    {
        ScheduleFabric.Holes.Clear();
        ScheduleFabric.Loners.Clear();
        ScheduleFabric.MistakeFinder.FindMistakes(ClassController.Classes);
    }

    public static void UpdateClassController()
    {
        ClassController.GetReadySchedule(ScheduleFabric.algControll);
    }
}
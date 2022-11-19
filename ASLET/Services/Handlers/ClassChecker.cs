using System.Collections.Generic;
using System.Drawing;
using ASLET.Services.Objects;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public class ClassChecker
{
    private List<string> _classesNameOrder;
    private SubjectExample[,,] _subjects;
    private readonly SubjectExample _empty = new SubjectExample();

    public void FindMistakes(Dictionary<string, Class> classes)
    {
        int numClasses = classes.Values.Count;
        _subjects = new SubjectExample[numClasses, 5, 7];
        _classesNameOrder = new List<string>();
        List<Class> classesList = new List<Class>(classes.Values);
        for (int i = 0; i < classesList.Count; i++)
        {
            _classesNameOrder.Add(classesList[i].Name);
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    _subjects[i, j, k] = new SubjectExample(classesList[i].Schedule[j, k].Subject,
                        classesList[i].Schedule[j, k].Teacher);
                }
            }
        }

        DictionaryUtils.PutAll(ScheduleFabric.Holes, CheckForHoles());
        DictionaryUtils.PutAll(ScheduleFabric.Loners, CheckForLoners());
    }

    private Dictionary<string, List<Point>> CheckForHoles()
    {
        Dictionary<string, List<Point>> holes = new Dictionary<string, List<Point>>();
        for (int i = 0; i < _classesNameOrder.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (_subjects[i, j, k].Equals(_empty))
                    {
                        if (k - 1 < 0 || !_subjects[i, j, k + 1].Equals(_empty))
                        {
                            if (!holes.ContainsKey(_classesNameOrder[i]))
                            {
                                DictionaryUtils.Put(holes, _classesNameOrder[i], new List<Point>());
                            }

                            holes[_classesNameOrder[i]].Add(new Point(j, k));
                            continue;
                        }

                        if (!_subjects[i, j, k + 1].Equals(_empty) && !_subjects[i, j, k - 1].Equals(_empty))
                        {
                            if (!holes.ContainsKey(_classesNameOrder[i]))
                            {
                                DictionaryUtils.Put(holes, _classesNameOrder[i], new List<Point>());
                            }

                            holes[_classesNameOrder[i]].Add(new Point(j, k));
                        }
                    }
                }
            }
        }

        return holes;
    }

    private Dictionary<string, List<Point>> CheckForLoners()
    {
        Dictionary<string, List<Point>> loners = new Dictionary<string, List<Point>>();
        for (int i = 0; i < _classesNameOrder.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 1; k < 6; k++)
                {
                    if (!_subjects[i, j, k].Equals(_empty) &&
                        (_subjects[i, j, k - 1].Equals(_empty) && _subjects[i, j, k + 1].Equals(_empty)))
                    {
                        if (!loners.ContainsKey(_classesNameOrder[i]))
                        {
                            DictionaryUtils.Put(loners, _classesNameOrder[i], new List<Point>());
                        }

                        loners[_classesNameOrder[i]].Add(new Point(j, k));
                    }
                }
            }
        }

        return loners;
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using ASLET.Services.Objects;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public class ClassChecker
{
    private List<Guid> _classesIdOrder;
    private SubjectExample[,,] _subjects;
    private readonly SubjectExample _empty = new SubjectExample();

    public void FindMistakes(Dictionary<Guid, Class> classes)
    {
        int numClasses = classes.Values.Count;
        _subjects = new SubjectExample[numClasses, 5, 7];
        _classesIdOrder = new List<Guid>();
        List<Class> classesList = new List<Class>(classes.Values);
        for (int i = 0; i < classesList.Count; i++)
        {
            _classesIdOrder.Add(classesList[i].Id);
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    _subjects[i, j, k] = new SubjectExample(classesList[i].Schedule[j, k].SubjectId, classesList[i].Schedule[j, k].TeacherId, classesList[i].Schedule[j, k].Subject,
                        classesList[i].Schedule[j, k].Teacher);
                }
            }
        }

        DictionaryUtils.PutAll(ScheduleFabric.Holes, CheckForHoles());
        DictionaryUtils.PutAll(ScheduleFabric.Loners, CheckForLoners());
    }

    private Dictionary<Guid, List<Point>> CheckForHoles()
    {
        Dictionary<Guid, List<Point>> holes = new Dictionary<Guid, List<Point>>();
        for (int i = 0; i < _classesIdOrder.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (_subjects[i, j, k].Equals(_empty))
                    {
                        if (k - 1 < 0 || !_subjects[i, j, k + 1].Equals(_empty))
                        {
                            if (!holes.ContainsKey(_classesIdOrder[i]))
                            {
                                DictionaryUtils.Put(holes, _classesIdOrder[i], new List<Point>());
                            }

                            holes[_classesIdOrder[i]].Add(new Point(j, k));
                            continue;
                        }

                        if (!_subjects[i, j, k + 1].Equals(_empty) && !_subjects[i, j, k - 1].Equals(_empty))
                        {
                            if (!holes.ContainsKey(_classesIdOrder[i]))
                            {
                                DictionaryUtils.Put(holes, _classesIdOrder[i], new List<Point>());
                            }

                            holes[_classesIdOrder[i]].Add(new Point(j, k));
                        }
                    }
                }
            }
        }

        return holes;
    }

    private Dictionary<Guid, List<Point>> CheckForLoners()
    {
        Dictionary<Guid, List<Point>> loners = new Dictionary<Guid, List<Point>>();
        for (int i = 0; i < _classesIdOrder.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 1; k < 6; k++)
                {
                    if (!_subjects[i, j, k].Equals(_empty) &&
                        (_subjects[i, j, k - 1].Equals(_empty) && _subjects[i, j, k + 1].Equals(_empty)))
                    {
                        if (!loners.ContainsKey(_classesIdOrder[i]))
                        {
                            DictionaryUtils.Put(loners, _classesIdOrder[i], new List<Point>());
                        }

                        loners[_classesIdOrder[i]].Add(new Point(j, k));
                    }
                }
            }
        }

        return loners;
    }
}
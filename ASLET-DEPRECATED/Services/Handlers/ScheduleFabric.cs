using System;
using System.Collections.Generic;
using System.Drawing;
using ASLET.Services.Utils;

namespace ASLET.Services.Handlers;

public static class ScheduleFabric
{
    public static Controller algControll; // TODO
    public static readonly ClassChecker MistakeFinder;
    public static readonly Dictionary<Guid, List<Point>> Holes;
    public static readonly Dictionary<Guid, List<Point>> Loners;

    static ScheduleFabric()
    {
        MistakeFinder = new ClassChecker();
        Holes = new Dictionary<Guid, List<Point>>();
        Loners = new Dictionary<Guid, List<Point>>();
    }
}
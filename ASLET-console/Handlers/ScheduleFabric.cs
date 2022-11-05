using System.Drawing;
using ASLET.Utils;

namespace ASLET.Handlers;

public static class ScheduleFabric
{
    public static Controller algControll;
    public static readonly ClassChecker MistakeFinder;
    public static readonly Dictionary<string, List<Point>> Holes;
    public static readonly Dictionary<string, List<Point>> Loners;

    static ScheduleFabric()
    {
        MistakeFinder = new ClassChecker();
        Holes = new Dictionary<string, List<Point>>();
        Loners = new Dictionary<string, List<Point>>();
    }
}
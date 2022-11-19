using System.Collections.Generic;

namespace ASLET.Services.Handlers;

public class TeacherHandler : IHandler
{
    public List<string> Teachers { get; }

    public TeacherHandler()
    {
        Teachers = new List<string>();
    }

    public void Add(string name)
    {
        Teachers.Add(name);
    }

    public void Remove(string name)
    {
        Teachers.Remove(name);
    }

    public bool Contains(string name)
    {
        return Teachers.Contains(name);
    }
}
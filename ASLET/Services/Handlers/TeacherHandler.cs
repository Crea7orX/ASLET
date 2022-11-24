using System;
using System.Collections.Generic;

namespace ASLET.Services.Handlers;

public class TeacherHandler : IHandler
{
    public List<Guid> Teachers { get; }

    public TeacherHandler()
    {
        Teachers = new List<Guid>();
    }

    public void Add(Guid id, string name)
    {
        Teachers.Add(id);
    }

    public void Remove(Guid id)
    {
        Teachers.Remove(id);
    }

    public bool Contains(Guid id)
    {
        return Teachers.Contains(id);
    }
}
using System;

namespace ASLET.Services.Handlers;

public interface IHandler
{
    public void Add(Guid id, string name);

    public void Remove(Guid id);

    public bool Contains(Guid id);
}
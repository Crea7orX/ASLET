namespace ASLET.Handlers;

public interface IHandler
{
    public void Add(string name);

    public void Remove(string name);

    public bool Contains(string name);
}
namespace ASLET.Handlers;

public class TeacherHandler : IHandler
{
    private List<string> Teachers { get; set; }

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
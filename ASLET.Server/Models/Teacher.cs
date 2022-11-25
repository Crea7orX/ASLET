namespace ASLET.Server.Models;

public class Teacher : Entity
{
    public List<Hour> Hours { get; set; }
    public string Name { get; set; }
}
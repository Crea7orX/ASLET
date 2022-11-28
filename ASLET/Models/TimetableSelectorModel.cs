namespace ASLET.Models;

public class TimetableSelectorModel
{
    public object Model { get; set; }
    public string DisplayName { get; set; }

    public TimetableSelectorModel(object model, string displayName)
    {
        Model = model;
        DisplayName = displayName;
    }
}
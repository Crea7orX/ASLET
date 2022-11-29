namespace ASLET.Models;

public class TimetableSelectorModel
{
    public static TimetableSelectorModel Empty = new TimetableSelectorModel(null, "");
    public object Model { get; set; }
    public string DisplayName { get; set; }

    public TimetableSelectorModel(object model, string displayName)
    {
        Model = model;
        DisplayName = displayName;
    }
}
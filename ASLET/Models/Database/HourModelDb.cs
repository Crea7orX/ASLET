using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASLET.Models.Database;

public class HourModelDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int NumberOfSeats { get; set; }
    public bool LabRequired { get; set; }
    public int HoursAWeek { get; set; }
    public TeacherModelDb Teacher { get; set; }
    public SubjectModelDb Subject { get; set; }
    public ClassModelDb Class { get; set; }

    public HourModelDb(string id, int numberOfSeats, bool labRequired, int hoursAWeek, TeacherModelDb teacher, SubjectModelDb subject, ClassModelDb @class)
    {
        Id = id;
        NumberOfSeats = numberOfSeats;
        LabRequired = labRequired;
        HoursAWeek = hoursAWeek;
        Teacher = teacher;
        Subject = subject;
        Class = @class;
    }
}
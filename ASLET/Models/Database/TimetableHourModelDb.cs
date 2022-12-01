using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASLET.Models.Database;

public class TimetableHourModelDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Hour { get; set; }
    public string Monday { get; set; }
    public string Tuesday { get; set; }
    public string Wednesday { get; set; }
    public string Thursday { get; set; }
    public string Friday { get; set; }

    public TimetableHourModelDb(string id, string hour, string monday, string tuesday, string wednesday, string thursday, string friday)
    {
        Id = id;
        Hour = hour;
        Monday = monday;
        Tuesday = tuesday;
        Wednesday = wednesday;
        Thursday = thursday;
        Friday = friday;
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASLET.Models.Database;

public class RoomModelDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Lab { get; set; }
    public int NumberOfSeats { get; set; }

    public RoomModelDb(string id, string name, bool lab, int numberOfSeats)
    {
        Id = id;
        Name = name;
        Lab = lab;
        NumberOfSeats = numberOfSeats;
    }
}
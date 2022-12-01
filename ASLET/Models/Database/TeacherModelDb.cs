using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASLET.Models.Database;

public class TeacherModelDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }

    public TeacherModelDb(string id, string name)
    {
        Id = id;
        Name = name;
    }
}
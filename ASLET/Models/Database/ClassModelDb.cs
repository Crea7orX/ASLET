using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASLET.Models.Database;

public class ClassModelDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public byte Grade { get; set; }
    public char? Letter { get; set; }
    public int NumberOfStudents { get; set; }

    public ClassModelDb(string id, string name, byte grade, char? letter, int numberOfStudents)
    {
        Id = id;
        Name = name;
        Grade = grade;
        Letter = letter;
        NumberOfStudents = numberOfStudents;
    }
}
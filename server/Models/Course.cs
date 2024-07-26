using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("CourseName")]
    public string CourseCode { get; set; }

    [BsonElement("Description")]
    public string CourseName { get; set; }
}

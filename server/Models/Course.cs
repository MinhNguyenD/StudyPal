using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models;

public class Course
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string CourseCode { get; set; }

    public string CourseName { get; set; }
}

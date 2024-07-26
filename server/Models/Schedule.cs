using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models; 

public class Schedule {
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid UserId { get; set; }
    public long TimeFrom { get; set; }
    public long TimeTo { get; set; }
    public string CourseId { get; set; }
}
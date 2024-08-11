using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

public class TutorSchedule
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required]
    [BsonElement("eventId")]
    public string eventId { get; set; }

    [Required]
    [BsonElement("username")]
    public string username { get; set; }

    [Required]
    [BsonElement("courseId")]
    public string CourseId { get; set; }

    [Required]
    [BsonElement("timeFrom")]
    public long TimeFrom { get; set; }

    [Required]
    [BsonElement("timeTo")]
    public long TimeTo { get; set; }

    [BsonElement("users")]
    public List<string> Users { get; set; }

    public TutorSchedule()
    {
        Users = new List<string>();
    }
}
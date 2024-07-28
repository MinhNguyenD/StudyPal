using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UserProfile
{
    [BsonId]
    [BsonIgnoreIfNull]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("firstname")]
    public string FirstName { get; set; }

    [BsonElement("lastname")]
    public string LastName { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }
}
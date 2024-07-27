using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class GroupChat
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("groupId")]
    public string GroupId { get; set; }

    [BsonElement("users")]
    public List<string> Users { get; set; }

    public GroupChat()
    {
        Users = new List<string>();
    }
}
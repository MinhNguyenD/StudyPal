using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models; 

public class Schedule {
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid UserId; // remove once user id is stored in the jwt token
    public long TimeFrom;
    public long TimeTo;
}
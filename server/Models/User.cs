using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace server.Models;

[CollectionName("Users")]
public class User : MongoIdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

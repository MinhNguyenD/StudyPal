using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace server.Models;

[CollectionName("Roles")]
public class Role : MongoIdentityRole<Guid>
{

}

using MongoDB.Driver;
using server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class UserProfileService
{
    private readonly IMongoCollection<UserProfile> _userProfiles;

    public UserProfileService(IMongoDatabase database)
    {
        _userProfiles = database.GetCollection<UserProfile>("UserProfiles");
    }

    public async Task<List<UserProfile>> GetUserProfilesByDetail(string detail)
    {
        return await _userProfiles.Find(profile => profile.FirstName.ToLower() == detail.ToLower() || profile.LastName.ToLower() == detail.ToLower()
            || profile.Email.ToLower() == detail.ToLower() || profile.Username.ToLower() == detail.ToLower()).ToListAsync();
    }

    public async Task<List<UserProfile>> GetAllUserProfiles()
    {
        return await _userProfiles.Find(_ => true).ToListAsync();
    }
    
    public async Task AddUser(UserProfile profile)
    {
        await _userProfiles.InsertOneAsync(profile);
    }

    public async Task<List<UserProfile>> GetProfilesNotUsername(string username)
    {
        return await _userProfiles.Find(profile => profile.Username.ToLower() != username.ToLower()).ToListAsync();
    }

    public async Task<UserProfile> GetUsernameFromEmail(string email)
    {
        return await _userProfiles.Find(profile => profile.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
    }
}
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GroupChatService
{
    private readonly IMongoCollection<GroupChat> _groupChats;

    public GroupChatService(IMongoDatabase database)
    {
        _groupChats = database.GetCollection<GroupChat>("GroupChats");
    }

    public async Task<GroupChat> CreateGroupChatAsync(GroupChat groupChat)
    {
        await _groupChats.InsertOneAsync(groupChat);
        return groupChat;
    }

    public async Task<GroupChat> GetGroupChatByIdAsync(string id)
    {
        return await _groupChats.Find(gc => gc.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<GroupChat>> GetAllGroupChatsAsync()
    {
        return await _groupChats.Find(_ => true).ToListAsync();
    }

    public async Task AddUserToGroupChatAsync(string groupId, string username)
    {
        var update = Builders<GroupChat>.Update.AddToSet(gc => gc.Users, username);
        await _groupChats.UpdateOneAsync(gc => gc.GroupId == groupId, update);
    }

    public async Task<List<GroupChat>> GetGroupChatsForUserAsync(string userId)
    {
        return await _groupChats.Find(gc => gc.Users.Contains(userId)).ToListAsync();
    }
}
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GroupChatService
{
    private readonly IMongoCollection<GroupChat> _groupChats;
        private readonly IMongoCollection<ChatMessage> _chatMessages;


    public GroupChatService(IMongoDatabase database)
    {
        _groupChats = database.GetCollection<GroupChat>("GroupChats");
        _chatMessages = database.GetCollection<ChatMessage>("Messages");
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

    public async Task<GroupChat> GetGroupChatByGroupIdAsync(string groupId)
    {
        return await _groupChats.Find(gc => gc.GroupId == groupId).FirstOrDefaultAsync();
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

    public async Task RemoveUserFromGroupChatAsync(string groupId, string username)
    {
        var remove = Builders<GroupChat>.Update.Pull(gc => gc.Users, username);
        await _groupChats.UpdateOneAsync(gc => gc.GroupId == groupId, remove);
    }

    public async Task DeleteGroupChatAsync(string groupId)
    {
        await _groupChats.DeleteOneAsync(gc => gc.GroupId == groupId);
    }

    public async Task DeleteAllGroupMessages(string groupId){
        await _chatMessages.DeleteManyAsync(m => m.ReceiverId.Equals(groupId));
    }
}
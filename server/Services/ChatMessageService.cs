using MongoDB.Driver;
using server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ChatMessageService
{
    private readonly IMongoCollection<ChatMessage> _messages;
    private readonly GroupChatService _groupChatService;

    public ChatMessageService(IMongoDatabase database, GroupChatService groupChatService)
    {
        _messages = database.GetCollection<ChatMessage>("Messages");
        _groupChatService = groupChatService;
    }

    public async Task<List<ChatMessage>> GetMessagesForUserAsync(string userId)
    {
        var userMessages = await _messages.Find(m => (m.SenderId == userId || m.ReceiverId == userId) && m.Type != MessageType.GroupMessage).ToListAsync();

        var groupChats = await _groupChatService.GetGroupChatsForUserAsync(userId);
        var groupIds = groupChats.Select(gc => gc.GroupId).ToList();

        var groupMessages = await _messages.Find(m => groupIds.Contains(m.ReceiverId)).ToListAsync();

        return userMessages.Concat(groupMessages).ToList();
    }

    public async Task<List<ChatMessage>> GetAllMessagesAsync()
    {
        return await _messages.Find(_ => true).ToListAsync();
    }

    public async Task SendMessageAsync(ChatMessage message)
    {
        await _messages.InsertOneAsync(message);
    }
}

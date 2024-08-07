using Microsoft.AspNetCore.Mvc;
using server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class GroupChatController : ControllerBase
{
    private readonly GroupChatService _groupChatService;

    public GroupChatController(GroupChatService groupChatService)
    {
        _groupChatService = groupChatService;
    }

    [HttpPost]
    public async Task<ActionResult<GroupChat>> CreateGroupChat(GroupChat groupChat)
    {
        var createdGroupChat = await _groupChatService.CreateGroupChatAsync(groupChat);
        return CreatedAtAction(nameof(GetGroupChatById), new { id = createdGroupChat.Id }, createdGroupChat);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupChat>> GetGroupChatById(string id)
    {
        var groupChat = await _groupChatService.GetGroupChatByIdAsync(id);

        if (groupChat == null)
        {
            return NotFound();
        }

        return groupChat;
    }

    [HttpGet]
    public async Task<ActionResult<List<GroupChat>>> GetAllGroupChats()
    {
        var groupChats = await _groupChatService.GetAllGroupChatsAsync();
        return Ok(groupChats);
    }

    [HttpPost("{groupId}/users/{username}")]
    public async Task<IActionResult> AddUserToGroupChat(string groupId, string username)
    {
        await _groupChatService.AddUserToGroupChatAsync(groupId, username);
        return NoContent();
    }

    [HttpDelete("{groupId}/users/{username}")]
    public async Task<IActionResult> RemoveUserFromGroupChat(string groupId, string username)
    {
        await _groupChatService.RemoveUserFromGroupChatAsync(groupId, username);
        var groupChat =  await _groupChatService.GetGroupChatByGroupIdAsync(groupId);
        if(groupChat.Users.Count == 0){
            await _groupChatService.DeleteGroupChatAsync(groupId);
            await _groupChatService.DeleteAllGroupMessages(groupId);
        }
        return NoContent();
    }
}
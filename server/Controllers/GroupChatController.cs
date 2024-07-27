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

    [HttpPost("{groupId}/users")]
    public async Task<IActionResult> AddUserToGroupChat(string groupId, [FromBody] string username)
    {
        await _groupChatService.AddUserToGroupChatAsync(groupId, username);
        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace server;

[ApiController]
[Route("api/[controller]")]
public class ChatMessageController : ControllerBase
{
    private readonly ChatMessageService _chatService;

    public ChatMessageController(ChatMessageService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("messages/{userId}")]
    public async Task<ActionResult<List<ChatMessage>>> GetMessagesForUser(string userId)
    {
        return await _chatService.GetMessagesForUserAsync(userId);
    }

    [HttpGet("messages")]
    public async Task<ActionResult<List<ChatMessage>>> GetAllMessages()
    {
        return await _chatService.GetAllMessagesAsync();
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
    {
        if (message == null)
        {
            return BadRequest("Invalid message data.");
        }

        message.Id = null;

        await _chatService.SendMessageAsync(message);
        return Ok();
    }

}
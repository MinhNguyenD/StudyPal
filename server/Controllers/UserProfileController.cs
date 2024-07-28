using Microsoft.AspNetCore.Mvc;
using server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly UserProfileService _userProfileService;

    public UserProfileController(UserProfileService userService)
    {
        _userProfileService = userService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddUser([FromBody] UserProfile profile)
    {
        if (profile == null)
        {
            return BadRequest("Invalid message data.");
        }

        profile.Id = null;

        await _userProfileService.AddUser(profile);
        return Ok();
    }

    [HttpGet("{detail}")]
    public async Task<ActionResult<List<UserProfile>>> GetUserProfilesByFirstName(string detail)
    {
        var profiles = await _userProfileService.GetUserProfilesByDetail(detail);
        if (profiles == null || profiles.Count == 0)
        {
            return NotFound();
        }
        return profiles;
    }

    [HttpGet("profiles")]
    public async Task<ActionResult<List<UserProfile>>> GetAllUserProfiles()
    {
        var profiles = await _userProfileService.GetAllUserProfiles();
        if (profiles == null)
        {
            return NotFound();
        }
        return profiles;
    }

    [HttpGet("profiles/not/{username}")]
    public async Task<ActionResult<List<UserProfile>>> GetProfilesNotUsername(string username)
    {
        var profiles = await _userProfileService.GetProfilesNotUsername(username);
        if (profiles == null)
        {
            return NotFound();
        }
        return profiles;
    }

    [HttpGet("profiles/{email}")]
    public async Task<ActionResult<UserProfile>> GetUsernameFromEmail(string email)
    {
        var profile = await _userProfileService.GetUsernameFromEmail(email);
        if (profile == null)
        {
            return NotFound();
        }
        return profile;
    }
}
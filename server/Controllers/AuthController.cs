using Microsoft.AspNetCore.Mvc;

namespace server;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    public AuthController()
    {

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {

    }
}

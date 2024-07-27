using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    /// <summary>
    /// Constructor for AuthController that initializes the authentication service.
    /// </summary>
    /// <param name="authService">Instance of the AuthService</param>
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerRequestDto">Registration request data</param>
    /// <returns>Action result indicating success or failure</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var registerResponse = await _authService.Register(registerRequestDto);
        if (!registerResponse.Succeeded)
        {
            return BadRequest(registerResponse.Errors);
        }
        return Ok(registerResponse.AuthUser);
    }


    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginRequestDto">Login request data</param>
    /// <returns>Action result indicating success or failure</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var loginResponse = await _authService.Login(loginRequestDto);
        if (!loginResponse.Succeeded)
        {
            return BadRequest(loginResponse.Errors);
        }
        return Ok(loginResponse.AuthUser);
    }
}

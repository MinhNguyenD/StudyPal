using Microsoft.AspNetCore.Mvc;
using server.Exceptions;
using server.Services;

namespace server;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JWTTokenService _tokenService;
    public AuthController(AuthService authService, JWTTokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newUser = await _authService.Register(registerRequestDto);

            return Ok(new AuthUserDto()
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Token = _tokenService.GenerateToken(newUser)
            });
        }
        catch (UserAlreadyExistsException e)
        {
            return StatusCode(400, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var user = await _authService.Login(loginRequestDto);

            return Ok(new AuthUserDto()
            {
                Username = user.Username,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            });
        }
        catch (InvalidCredentialsException e)
        {
            return StatusCode(401, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}

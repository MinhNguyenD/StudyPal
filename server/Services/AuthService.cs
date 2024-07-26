using Microsoft.AspNetCore.Identity;
using server.Models;

namespace server.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signinManager;

    private readonly JWTTokenService _tokenService;
    public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, JWTTokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signinManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> Register(RegisterRequestDto registerRequestDto)
    {
        var newUser = new User()
        {
            UserName = registerRequestDto.Username,
            Email = registerRequestDto.Email,
            FirstName = registerRequestDto.FirstName,
            LastName = registerRequestDto.LastName
        };
        var createUserResult = await _userManager.CreateAsync(newUser, registerRequestDto.Password);
        if (!createUserResult.Succeeded)
        {

            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = createUserResult.Errors,
            };
        }
        var roleResult = await AddRolesToUser(newUser, registerRequestDto.Roles);
        if (!roleResult.Succeeded)
        {
            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = roleResult.Errors,
            };
        }
        return new AuthResponseDto
        {
            Succeeded = true,
            AuthUser = new AuthUserDto
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Username = newUser.UserName,
                Email = newUser.Email,
                Token = _tokenService.GenerateToken(newUser)
            }
        };
    }

    private async Task<IdentityResult> AddRolesToUser(User user, List<string> roles)
    {
        foreach (string role in roles)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new Role
                {
                    Name = role
                });
            }
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                return roleResult;
            }
        }
        return IdentityResult.Success;
    }
    public async Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
        if (user == null)
        {
            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = ["Invalid Credentials. Email or Password does not match"],
            };
        }
        var loginResult = await _signinManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
        if (!loginResult.Succeeded)
        {
            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = ["Invalid Credentials. Email or Password does not match"],
            };
        }
        return new AuthResponseDto
        {
            Succeeded = true,
            AuthUser = new AuthUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            },
        };
    }
}

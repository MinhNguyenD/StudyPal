using Microsoft.AspNetCore.Identity;
using server.Models;

namespace server.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signinManager;

    private readonly JWTTokenService _tokenService;

    /// <summary>
    /// Constructor for dependency injection
    /// </summary>
    /// <param name="userManager">UserManager instance</param>
    /// <param name="roleManager">RoleManager instance</param>
    /// <param name="signInManager">SignInManager instance</param>
    /// <param name="tokenService">JWTTokenService instance</param>
    public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, JWTTokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signinManager = signInManager;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerRequestDto">Registration request data</param>
    /// <returns>Authentication response containing success status and user info</returns>
    public async Task<AuthResponseDto> Register(RegisterRequestDto registerRequestDto)
    {
        var errors = new List<object>();
        
        // Check if username is already taken
        var existingUsername = await _userManager.FindByNameAsync(registerRequestDto.Username);
        if (existingUsername != null)
        {
            errors.Add(new {
                    Code = "DuplicateUserName",
                    Description = $"Username '{registerRequestDto.Username}' is already taken"
                });
        }

        // Check if email is already registered
        var existingUser = await _userManager.FindByEmailAsync(registerRequestDto.Email);
        if (existingUser != null)
        {
            errors.Add(new {
                    Code = "DuplicateEmail",
                    Description = $"Email '{registerRequestDto.Email}' is already taken"
                });
        }

        // If there are any errors, return them
        if(errors.Count > 0){
            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = errors,
            };
        }
        
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

    /// <summary>
    /// Adds roles to the specified user.
    /// </summary>
    /// <param name="user">User to add roles to</param>
    /// <param name="roles">List of roles to add</param>
    /// <returns>IdentityResult indicating success or failure</returns>
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

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginRequestDto">Login request data</param>
    /// <returns>Authentication response containing success status and user info</returns>
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

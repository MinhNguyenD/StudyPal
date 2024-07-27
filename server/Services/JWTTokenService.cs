using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.Models;

namespace server.Services;

public class JWTTokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    /// <summary>
    /// Constructor for JWTTokenService that initializes the configuration and security key.
    /// </summary>
    /// <param name="config">Configuration instance</param>
    public JWTTokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
    }


    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">User for whom the token is generated</param>
    /// <returns>A JWT token as a string</returns>
    public string GenerateToken(User user)
    {
        // Create claims based on user properties
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var signingCred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(3),
            SigningCredentials = signingCred,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };
        // Create the token handler and generate the token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        // Return the token as a string
        return tokenHandler.WriteToken(token);
    }

}

using System.ComponentModel.DataAnnotations;

namespace server;

public class RegisterRequestDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public List<string> Roles { get; set; }

    [Required]
    public string Password { get; set; }
}

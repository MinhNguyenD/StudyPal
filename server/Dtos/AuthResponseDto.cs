namespace server;

public class AuthResponseDto
{
    public bool Succeeded { get; set; }
    public AuthUserDto? AuthUser { get; set; }
    public IEnumerable<object>? Errors { get; set; }
}

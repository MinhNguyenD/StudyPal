using MongoDB.Driver;
using server.Exceptions;
using server.Models;

namespace server.Services;

public class AuthService
{
    private readonly IMongoClient _mongoClient;
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<User> _userCollection;
    public AuthService(IConfiguration config, IMongoClient mongoClient)
    {
        _config = config;
        _mongoClient = mongoClient;
        _db = _mongoClient.GetDatabase(_config["MongoDB:DatabaseName"]);
        _userCollection = _db.GetCollection<User>("Users");
    }

    public async Task<User> Register(RegisterRequestDto registerRequestDto)
    {
        var newUser = new User()
        {
            Username = registerRequestDto.Username,
            Email = registerRequestDto.Email,
            FirstName = registerRequestDto.FirstName,
            LastName = registerRequestDto.LastName,
            Password = registerRequestDto.Password,
        };
        var existedUser = await _userCollection.Find(x => x.Email.Equals(newUser.Email)).FirstOrDefaultAsync();

        if (existedUser != null)
        {
            throw new UserAlreadyExistsException("Email already exists");
        }
        existedUser = await _userCollection.Find(x => x.Username.Equals(newUser.Username)).FirstOrDefaultAsync();
        if (existedUser != null)
        {
            throw new UserAlreadyExistsException("Username already exists");
        }
        await _userCollection.InsertOneAsync(newUser);
        return newUser;
    }

    public async Task<User> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _userCollection.Find(x => x.Email.Equals(loginRequestDto.Email)).FirstOrDefaultAsync();
        if (user == null || !user.Password.Equals(loginRequestDto.Password))
        {
            throw new InvalidCredentialsException("Invalid credentials. Email or password does not match");
        }
        return user;
    }
}

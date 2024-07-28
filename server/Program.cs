using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using server.Models;
using server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(builder.Configuration["MongoDB:AtlasURI"]));
builder.Services.AddTransient<UserService>();
//add mongoIdentityConfiguration...
var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = builder.Configuration["MongoDB:AtlasURI"],
        DatabaseName = builder.Configuration["MongoDB:DatabaseName"]
    },
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
    }
};

builder.Services.ConfigureMongoDbIdentity<User, Role, Guid>(mongoDbIdentityConfig)
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>()
    .AddRoleManager<RoleManager<Role>>();
builder.Services.AddScoped<JWTTokenService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddSingleton(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var database = mongoClient.GetDatabase(builder.Configuration["MongoDB:DatabaseName"]);
    var groupChatService = sp.GetRequiredService<GroupChatService>();
    return new ChatMessageService(database, groupChatService);
});

builder.Services.AddSingleton(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var database = mongoClient.GetDatabase(builder.Configuration["MongoDB:DatabaseName"]);
    return new GroupChatService(database);
});

builder.Services.AddSingleton(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var database = mongoClient.GetDatabase(builder.Configuration["MongoDB:DatabaseName"]);
    return new UserProfileService(database);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])
        )
    };
});

var app = builder.Build();
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader());
// app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
Console.WriteLine("listening"); // doesn't work 😔
System.Diagnostics.Debug.WriteLine("listening");  // doesn't work 😔
app.Run();

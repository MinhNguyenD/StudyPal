// THIS IS NOT IN USE RIGHT NOW. IF THE TEAM WANT TO USE EF CORE, UNCOMMENT THIS
/*
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using server.Models;

namespace server;

public class ApplicationDBContext : IdentityDbContext
{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<User> Users { get; init; }
    public static ApplicationDBContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToCollection("Users");
    }
}
*/
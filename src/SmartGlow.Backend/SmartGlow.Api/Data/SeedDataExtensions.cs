using Microsoft.EntityFrameworkCore;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Persistence.DataContext;

namespace SmartGlow.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
        
        if (!await appDbContext.Users.AnyAsync())
            await appDbContext.SeedUsersAsync();
        
        if (appDbContext.ChangeTracker.HasChanges())
            await appDbContext.SaveChangesAsync();
    }
    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext)
    {
        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
                FirstName = "John",
                LastName = "Doe",
                UserName = "jhonDoe",
                PhoneNumber = "+9989326922343",
                AttachedStreets = ["qora qamish 2/4","qora qamish 2/5"],
                PasswordHash = "$2a$12$pHdneNbJGp4SnN1ovHrNqevf6I.k3Gy.7OMJoWWB0RByv0foi4fgy", // qwerty123
                Role = RoleType.User
            },
            new()
            {
                Id = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d"),
                FirstName = "Bob",
                LastName = "Richard",
                UserName = "Administrator",
                PhoneNumber = "+9989326955343",
                AttachedStreets = ["qora qamish 2/5","qora qamish 2/4"],
                PasswordHash = "$2a$12$LxSqe5AE7AtglesHHK5NROFdJQdA1r1XKqhzg4q/tMTZjVEH0PNSK", //asdf1234
                Role = RoleType.Admin
            },
            new()
            {
                Id = Guid.Parse("6357D344-CB69-4FAA-81C5-AC0FC59AE0F9"),
                FirstName = "Sarah",
                LastName = "Funk",
                UserName = "SarahFunk",
                PhoneNumber = "+9989326922343",
                AttachedStreets = ["qora qamish 2/1","qora qamish 2/5"],
                PasswordHash = "$2a$12$LxSqe5AE7AtglesHHK5NROFdJQdA1r1XKqhzg4q/tMTZjVEH0PNSK", //asdf1234
                Role = RoleType.User
            }
        };

        await dbContext.Users.AddRangeAsync(users);
    }
}
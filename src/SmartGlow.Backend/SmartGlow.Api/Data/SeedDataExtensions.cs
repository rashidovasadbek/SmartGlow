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
        
        if (!await appDbContext.Streets.AnyAsync())
            await appDbContext.SeedStreetsAsync();
        
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

    private static async ValueTask SeedStreetsAsync(this AppDbContext dbContext)
    {
        var streets = new List<Street>
        {
            new()
            {
                Id = Guid.Parse("c2fe1019-1180-4f3e-b477-413a9b33bbd1"),
                StreetName = "qora qamish 2/4",
                Latitude = 37.7749,
                Longitude = -122.4194,
                UserId = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
            },
            
            new()
            {
                Id = Guid.Parse("0659846d-07b7-45ab-adaf-132f759eb482"),
                StreetName = "qora qamish 2/5",
                Latitude = 39.7749,
                Longitude = -149.4194,
                UserId = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d"),
            },
            
            new()
            {
                Id = Guid.Parse("971a4b1b-477b-4484-88d4-90a689651269"),
                StreetName = "qora qamish 2/1",
                Latitude = 27.7742,
                Longitude = -100.4134,
                UserId = Guid.Parse("6357D344-cb69-4faa-81c5-ac0fc59ae0f9"),
            },
            
            new()
            {
                Id = Guid.Parse("d2155298-792f-47a6-8d10-4715116b318d"),
                StreetName = "qora qamish 2/3",
                Latitude = 17.7752,
                Longitude = -98.4187,
                UserId = Guid.Parse("6357D344-CB69-4FAA-81C5-AC0FC59AE0F9"),
            },
            
            new()
            {
                Id = Guid.Parse("b7022e8b-54ec-4ae8-a466-e4df295d862c"),
                StreetName = "Chorsu 13/51",
                Latitude = 16.7752,
                Longitude = -78.4187,
                UserId = Guid.Parse("6357D344-CB69-4FAA-81C5-AC0FC59AE0F9"),
            },
            
            new()
            {
                Id = Guid.Parse("bc606dd5-0b9c-444d-aad3-6d527c76a16b"),
                StreetName = "Chilonzor 11",
                Latitude = 11.7752,
                Longitude = -28.4187,
                UserId = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
            }
        };
        
        await dbContext.Streets.AddRangeAsync(streets);
    }
}
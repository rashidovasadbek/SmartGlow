using BC = BCrypt.Net.BCrypt;
using SmartGlow.Application.Common.Identity.Services;

namespace SmartGlow.Infrastructure.Common.Identity.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    public bool ValidatePassword(string password, string hashPassword)
    {
        return BC.Verify(password, hashPassword);
    }
}
using SmartGlow.Application.Users.Models;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Application.Users.Queries;

/// <summary>
/// Represents a command to retrieve a client by their unique identifier.
/// </summary>
public record UserGetByIdQuery: IQuery<UserDto?>
{
    public Guid UserId { get; init; }
}
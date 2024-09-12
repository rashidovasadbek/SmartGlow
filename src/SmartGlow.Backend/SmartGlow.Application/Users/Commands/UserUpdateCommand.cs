using SmartGlow.Application.Users.Models;
using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Application.Users.Commands;

/// <summary>
/// Represents a command to update a client entity.
/// </summary>
public class UserUpdateCommand: ICommand<UserDto>
{
    /// <summary>
    /// Contains the updated organization data. 
    /// </summary>
    public UserDto User { get; init; } = default!;
}
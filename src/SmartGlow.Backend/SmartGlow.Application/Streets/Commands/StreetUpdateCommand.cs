using SmartGlow.Application.Streets.Models;
using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Application.Streets.Commands;

/// <summary>
/// Represents a command  to modify an existing street's information.
/// </summary>
public record StreetUpdateCommand : ICommand<StreetDto>
{
    /// <summary>
    /// Contains the updated Street information
    /// </summary>
    public StreetDto Street { get; set; } = default!;
}
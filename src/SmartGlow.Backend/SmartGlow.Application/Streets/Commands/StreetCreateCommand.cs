using SmartGlow.Application.Streets.Models;
using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Application.Streets.Commands;

/// <summary>
/// Represents a command to create a new street. 
/// </summary>
public record StreetCreateCommand : ICommand<StreetDto?>
{
    /// <summary>
    /// Represents a command to create a new street. 
    /// </summary>
    public StreetDto Street { get; set; } = default!;
}
using SmartGlow.Domain.Common.Entities;

namespace SmartGlow.Domain.Common.Commands;

/// <summary>
/// Represents a options to configure command execution and persistence behavior
/// </summary>
public struct CommandOptions ()
{
    /// <summary>
    /// Gets or sets persistence step behavior of command execution
    /// </summary>
    public bool SkipSavingChanges { get; set; } = false;
}
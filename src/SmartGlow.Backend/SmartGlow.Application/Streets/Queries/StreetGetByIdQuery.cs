using SmartGlow.Application.Streets.Models;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Application.Streets.Queries;

/// <summary>
/// Represents a query to retrieve a specific organization by its ID.
/// </summary>
public class StreetGetByIdQuery : IQuery<StreetDto?>
{
    /// <summary>
    /// The unique identifier of the street to retrieve.
    /// </summary>
    public Guid StreetId { get; set; }
}
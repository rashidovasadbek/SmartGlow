using SmartGlow.Application.Streets.Models;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Application.Streets.Queries;

/// <summary>
///  Represents a query to retrieve a collection of organizations.
/// </summary>
public record StreetGetQuery : IQuery<ICollection<StreetDto>>
{
   /// <summary>
   ///  Gets or sets the filter to apply when retrieving organizations.
   /// </summary>
   public StreetFilter StreetFilter { get; set; } = default!;
}
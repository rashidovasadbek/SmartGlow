using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Application.Streets.Models;

public class StreetFilter : FilterPagination
{
    /// <summary>
    /// Gets or sets street owner user id.
    /// </summary>
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Overrides base GetHashCode method
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Overrides base Equals method
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => 
        obj is StreetFilter streetFilter 
        && streetFilter.GetHashCode() == GetHashCode();
}
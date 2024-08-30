using Microsoft.EntityFrameworkCore;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Persistence.Extensions;

public static class EfCoreExtensions
{
    /// <summary>
    /// Applies pagination to given query source
    /// </summary>
    /// <param name="source">Queryable source</param>
    /// <param name="trackingMode">Tracking mode to apply</param>
    /// <typeparam name="TSource">Query source type</typeparam>
    /// <returns>Query source with pagination applied</returns>
    public static IQueryable<TSource> ApplyTrackingMode<TSource>(this IQueryable<TSource> source,
        QueryTrackingMode trackingMode) where TSource: class
    {
        return trackingMode switch
        {
            QueryTrackingMode.AsTracking => source,
            QueryTrackingMode.AsNoTracking => source.AsNoTracking(),
            QueryTrackingMode.AsNoTrackingWithIdentityResolution => source.AsNoTrackingWithIdentityResolution(),
            _ => source
        };
    }
}
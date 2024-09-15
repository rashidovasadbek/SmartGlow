using System.Linq.Expressions;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.Repositories.Interfaces;

public interface IOnOffTimingRepository
{
        /// <summary>
    /// Gets a queryable source of onOffTiming based on an optional predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter streets.</param>
    /// <param name="queryOptions">Query options</param>
    /// <returns>Queryable source of OnOffTiming</returns>
    IQueryable<OnOffTiming> Get(Expression<Func<OnOffTiming, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Gets a onOffTiming  by their ID.
    /// </summary>
    /// <param name="onOfTimingId">The ID of the onOffTiming.</param>
    /// <param name="queryOptions">Query options for sorting, paging, etc.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>OnOffTiming if found, otherwise null</returns>
    ValueTask<OnOffTiming?> GetByIdAsync(Guid onOfTimingId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if onOffTiming exists
    /// </summary>
    /// <param name="queryableSource">Queryable source delegate with predicate and other actionsk Delegate to apply LINQ methods on IQueryable source of OnOffTiming</param>
    /// <param name="expectedValue">Expected value to check against the result</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if OnOffTiming exists, otherwise false</returns>
    ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a OnOffTiming
    /// </summary>
    /// <param name="onOffTiming">The OnOffTiming to be created.</param>
    /// <param name="commandOptions">Create command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created OnOffTiming</returns>
    ValueTask<OnOffTiming> CreateAsync(OnOffTiming onOffTiming, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing onOffTiming
    /// </summary>
    /// <param name="onOffTiming">The user to be updated.</param>
    /// <param name="commandOptions">Update command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated street</returns>
    ValueTask<OnOffTiming> UpdateAsync(OnOffTiming onOffTiming, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a onOffTiming by their Id
    /// </summary>
    /// <param name="onOffTimingId">The Id of the onOffTiming to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated onOffTiming if soft deleted, otherwise null</returns>
    ValueTask<OnOffTiming?> DeleteByIdAsync(Guid onOffTimingId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a onOffTiming
    /// </summary>
    /// <param name="onOffTiming">The onOffTiming to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated user if soft deleted, otherwise null</returns>
    ValueTask<OnOffTiming?> DeleteAsync(OnOffTiming onOffTiming, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

}
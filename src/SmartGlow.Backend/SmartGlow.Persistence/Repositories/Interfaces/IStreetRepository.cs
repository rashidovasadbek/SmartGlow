using System.Linq.Expressions;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.Repositories.Interfaces;

public interface IStreetRepository
{
         /// <summary>
    /// Gets a queryable source of street based on an optional predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter streets.</param>
    /// <param name="queryOptions">Query options</param>
    /// <returns>Queryable source of users</returns>
    IQueryable<Street> Get(Expression<Func<Street, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Gets a single user by their ID.
    /// </summary>
    /// <param name="streetId">The ID of the street.</param>
    /// <param name="queryOptions">Query options for sorting, paging, etc.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>User if found, otherwise null</returns>
    ValueTask<Street?> GetByIdAsync(Guid streetId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if street exists
    /// </summary>
    /// <param name="queryableSource">Queryable source delegate with predicate and other actionsk Delegate to apply LINQ methods on IQueryable source of User</param>
    /// <param name="expectedValue">Expected value to check against the result</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if user exists, otherwise false</returns>
    ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a user
    /// </summary>
    /// <param name="street">The user to be created.</param>
    /// <param name="commandOptions">Create command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created street</returns>
    ValueTask<Street> CreateAsync(Street street, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="street">The user to be updated.</param>
    /// <param name="commandOptions">Update command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated street</returns>
    ValueTask<Street> UpdateAsync(Street street, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a street by their Id
    /// </summary>
    /// <param name="streetId">The Id of the user to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated street if soft deleted, otherwise null</returns>
    ValueTask<Street?> DeleteByIdAsync(Guid streetId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a street
    /// </summary>
    /// <param name="street">The user to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated user if soft deleted, otherwise null</returns>
    ValueTask<Street?> DeleteAsync(Street street, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

}
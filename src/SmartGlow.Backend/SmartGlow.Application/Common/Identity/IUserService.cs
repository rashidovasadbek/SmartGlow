using System.Linq.Expressions;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;

namespace SmartGlow.Application.Common.Identity;

/// <summary>
/// Defines foundation service for users
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets a queryable source of users based on an optional predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter users.</param>
    /// <param name="queryOptions">Query options.</param>
    /// <returns>Queryable source of users.</returns>
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Gets a single user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="queryOptions">Query options for sorting, paging, etc.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>User if found, otherwise null.</returns>
    ValueTask<User?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single user's phone number by their ID.
    /// </summary>
    /// <param name="userId">The Id of the user.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Phone number of the user if found, otherwise null</returns>
    ValueTask<string?> GetPhoneNumberByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single user Id by their phone number.
    /// </summary>
    /// <param name="phoneNumber">The phone number of the user.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>User Id if found, otherwise null</returns>
    ValueTask<Guid?> GetIdByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user exists by their Id
    /// </summary>
    /// <param name="userId">The Id of user to check</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if a user with the given Id exists, otherwise false</returns>
    ValueTask<bool> CheckByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user exists by their phone number
    /// </summary>
    /// <param name="phoneNumber">The phone number of the user to check</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if a user with the given phone number exists, otherwise false</returns>
    ValueTask<bool> CheckByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets system user Id.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>System user Id</returns>
    ValueTask<Guid> GetSystemUserIdAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to be created.</param>
    /// <param name="roleType">The user role</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created user.</returns>
    ValueTask<User> CreateAsync(User user, RoleType roleType, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user to be updated.</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated user.</returns>
    ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user to be deleted.</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>User if found and deleted, otherwise null.</returns>
    ValueTask<User?> DeleteByIdAsync(Guid userId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="user">The user to be deleted.</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>User if found and deleted, otherwise null.</returns>
    ValueTask<User?> DeleteAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

}
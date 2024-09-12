using System.Linq.Expressions;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;
using SmartGlow.Persistence.Caching.Brokers;
using SmartGlow.Persistence.DataContext;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Persistence.Repositories;

/// <summary>
/// Provides user repository functionality
/// </summary>
public class UserRepository(AppDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<User, AppDbContext>(dbContext, cacheBroker), IUserRepository
{
    public new  IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<User?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userId, queryOptions, cancellationToken);
    }

    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        return base.CheckAsync(queryableSource, expectedValue, cancellationToken);
    }

    public new ValueTask<User> CreateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(user, commandOptions, cancellationToken);
    }

    public new  ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(user, commandOptions, cancellationToken);
    }

    public new ValueTask<User?> DeleteByIdAsync(Guid userId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
       return base.DeleteByIdAsync(userId, commandOptions, cancellationToken);
    }

    public new ValueTask<User?> DeleteAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(user, commandOptions, cancellationToken);
    }
}
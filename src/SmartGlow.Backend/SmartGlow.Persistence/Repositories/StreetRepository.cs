using System.Linq.Expressions;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;
using SmartGlow.Persistence.Caching.Brokers;
using SmartGlow.Persistence.DataContext;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Persistence.Repositories;

/// <summary>
/// Provides street repository functionality
/// </summary>
public class StreetRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<Street, AppDbContext>(appDbContext, cacheBroker), IStreetRepository
{
    public new IQueryable<Street> Get(Expression<Func<Street, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<Street?> GetByIdAsync(Guid streetId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(streetId, queryOptions, cancellationToken);
    }

    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        return base.CheckAsync(queryableSource, expectedValue);
    }

    public new ValueTask<Street> CreateAsync(Street street, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(street, commandOptions, cancellationToken);
    }

    public new ValueTask<Street> UpdateAsync(Street street, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
       return base.UpdateAsync(street, commandOptions, cancellationToken);
    }

    public new ValueTask<Street?> DeleteByIdAsync(Guid streetId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
       return base.DeleteByIdAsync(streetId, commandOptions, cancellationToken);
    }

    public new ValueTask<Street?> DeleteAsync(Street street, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(street, commandOptions, cancellationToken);
    }
}
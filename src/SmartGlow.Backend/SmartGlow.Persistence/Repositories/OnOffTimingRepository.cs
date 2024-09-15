using System.Linq.Expressions;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;
using SmartGlow.Persistence.Caching.Brokers;
using SmartGlow.Persistence.DataContext;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Persistence.Repositories;

public class OnOffTimingRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<OnOffTiming, AppDbContext>(appDbContext, cacheBroker), IOnOffTimingRepository
{
    public new IQueryable<OnOffTiming> Get(Expression<Func<OnOffTiming, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<OnOffTiming?> GetByIdAsync(Guid onOfTimingId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(onOfTimingId, queryOptions, cancellationToken);
    }

    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        return base.CheckAsync<TValue>(queryableSource, expectedValue);
    }

    public new ValueTask<OnOffTiming> CreateAsync(OnOffTiming onOffTiming, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(onOffTiming, commandOptions, cancellationToken);
    }

    public ValueTask<OnOffTiming> UpdateAsync(OnOffTiming onOffTiming, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(onOffTiming, commandOptions, cancellationToken);
    }

    public new ValueTask<OnOffTiming?> DeleteByIdAsync(Guid onOffTimingId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(onOffTimingId, commandOptions, cancellationToken);
    }

    public new ValueTask<OnOffTiming?> DeleteAsync(OnOffTiming onOffTiming, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(onOffTiming, commandOptions, cancellationToken);
    }
}
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

public abstract class Repository<TAggregate, TIdentity>(DbContext dbContext)
    : IRepository<TAggregate, TIdentity> where TAggregate : Entity<TIdentity>
{
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    #region Implementation of IRepository<TAggregate>

    public IQueryable<TAggregate> AsQueryable()
    {
        return _dbContext.Set<TAggregate>();
    }

    public async Task Add(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(aggregate, cancellationToken);
    }

    public Task Update(TAggregate aggregate)
    {
        return Task.FromResult(_dbContext.Update(aggregate));
    }

    public Task Remove(TAggregate aggregate)
    {
        return Task.FromResult(_dbContext.Remove(aggregate));
    }

    public Task RemoveRange(IEnumerable<TAggregate> aggregates)
    {
        _dbContext.RemoveRange(aggregates);
        return Task.CompletedTask;
    }

    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion Implementation of IRepository<TAggregate>
}
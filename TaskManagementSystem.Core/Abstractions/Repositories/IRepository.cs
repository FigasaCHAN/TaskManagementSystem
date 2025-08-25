using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IRepository<TAggregate, TIdentity> where TAggregate : Entity<TIdentity>
{
    IQueryable<TAggregate> AsQueryable();

    Task Add(TAggregate aggregate, CancellationToken cancellationToken = default);

    Task Update(TAggregate aggregate);

    Task Remove(TAggregate aggregate);

    Task RemoveRange(IEnumerable<TAggregate> aggregates);

    Task SaveChanges(CancellationToken cancellationToken = default);
}
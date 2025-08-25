using TaskManagementSystem.Core.Abstractions;

namespace TaskManagementSystem.Core.Common;

public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>, IAggregateRoot
{
    private readonly ICollection<IDomainEvent> _events = [];

    protected AggregateRoot(TIdentity id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    public void AddEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }

    public IEnumerable<IDomainEvent> PopDomainEvents()
    {
        var copy = _events.ToList();
        _events.Clear();
        return copy;
    }
}
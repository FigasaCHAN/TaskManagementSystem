namespace TaskManagementSystem.Core.Abstractions;

public interface IAggregateRoot
{
    public void AddEvent(IDomainEvent domainEvent);

    public IEnumerable<IDomainEvent> PopDomainEvents();
}
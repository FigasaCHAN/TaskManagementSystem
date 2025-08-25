using TaskManagementSystem.Core.Abstractions;

namespace TaskManagementSystem.Core.Domain.Events.Task.Assigned;

public class TaskAssignedDomainEvent : IDomainEvent
{
    public TaskAssignedDomainEvent(AggregateRoots.Task task, int oldAssignedUserId, int newAssignedUserId,
        int changedBy,
        DateTime changedAt)
    {
        Task = task;
        OldAssignedUserId = oldAssignedUserId;
        NewAssignedUserId = newAssignedUserId;
        ChangedBy = changedBy;
        ChangedAt = changedAt;
    }

    public AggregateRoots.Task Task { get; }
    public int OldAssignedUserId { get; }
    public int NewAssignedUserId { get; }
    public int ChangedBy { get; }
    public DateTime ChangedAt { get; }
}
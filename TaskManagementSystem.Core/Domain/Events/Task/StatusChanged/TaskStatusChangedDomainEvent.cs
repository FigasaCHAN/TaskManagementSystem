using TaskManagementSystem.Core.Abstractions;
using TaskStatus = TaskManagementSystem.Core.Enums.Task.TaskStatus;

namespace TaskManagementSystem.Core.Domain.Events.Task.StatusChanged;

public class TaskStatusChangedDomainEvent : IDomainEvent
{
    public TaskStatusChangedDomainEvent(AggregateRoots.Task task, TaskStatus oldStatus, TaskStatus newStatus,
        int changedBy,
        DateTime changedAt)
    {
        Task = task;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedBy = changedBy;
        ChangedAt = changedAt;
    }

    public AggregateRoots.Task Task { get; }
    public TaskStatus OldStatus { get; }
    public TaskStatus NewStatus { get; }
    public int ChangedBy { get; }
    public DateTime ChangedAt { get; }
}
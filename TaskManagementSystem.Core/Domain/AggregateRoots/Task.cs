using TaskManagementSystem.Core.Common;
using TaskManagementSystem.Core.Domain.Entities.Task;
using TaskManagementSystem.Core.Domain.Events.Task.Assigned;
using TaskManagementSystem.Core.Domain.Events.Task.StatusChanged;
using TaskManagementSystem.Core.Domain.ValueObjects.Task;
using TaskManagementSystem.Core.Enums.Task;
using TaskStatus = TaskManagementSystem.Core.Enums.Task.TaskStatus;

namespace TaskManagementSystem.Core.Domain.AggregateRoots;

public class Task : AggregateRoot<int>
{
    private Task()
    {
    }

    public Task(string title, string description, int assignedUserId, int createdBy)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title), $"{nameof(Title)} cannot be null.");
        Description = description ??
                      throw new ArgumentNullException(nameof(description), $"{nameof(Description)} cannot be null.");
        AssignedUserId = assignedUserId;

        CreatedBy = CreatedInfo.Create(createdBy);
        Status = TaskStatus.Pending;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatus Status { get; private set; }
    public CreatedInfo CreatedBy { get; private set; }
    public LastModifiedInfo? LastModified { get; private set; }
    public int AssignedUserId { get; private set; }
    public bool Deleted { get; private set; }
    public long Version { get; private set; }
    public ICollection<TaskHistory> History { get; } = new List<TaskHistory>();

    public void UpdateDetails(string title, string description, int modifiedBy)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title), $"{nameof(Title)} cannot be null.");
        Description = description ??
                      throw new ArgumentNullException(nameof(description), $"{nameof(Description)} cannot be null.");

        LastModified = LastModifiedInfo.Create(modifiedBy);
    }

    public void Assign(int assignedUserId, int modifiedBy)
    {
        var oldAssignedUserId = AssignedUserId;
        AssignedUserId = assignedUserId;
        LastModified = LastModifiedInfo.Create(modifiedBy);
        AddEvent(new TaskAssignedDomainEvent(
            this,
            oldAssignedUserId,
            assignedUserId,
            modifiedBy,
            LastModified.LastModifiedAt ?? DateTime.UtcNow
        ));
    }

    public void ChangeStatus(TaskStatus newStatus, int modifiedBy)
    {
        var oldStatus = Status;
        Status = newStatus;
        LastModified = LastModifiedInfo.Create(modifiedBy);
        AddEvent(new TaskStatusChangedDomainEvent(
            this,
            oldStatus,
            newStatus,
            modifiedBy,
            LastModified.LastModifiedAt ?? DateTime.UtcNow
        ));
    }

    public void Delete(int modifiedBy)
    {
        LastModified = LastModifiedInfo.Create(modifiedBy);
        Deleted = true;
    }

    public void AddHistory(TaskEventType eventType, string? oldValue, string? newValue, int changedBy,
        DateTime changedAt)
    {
        var historyEntry = new TaskHistory
        {
            TaskId = Id,
            EventType = eventType.ToString(),
            OldValue = oldValue,
            NewValue = newValue,
            ChangedBy = changedBy,
            ChangedAt = changedAt
        };
        History.Add(historyEntry);
    }
}
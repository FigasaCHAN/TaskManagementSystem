using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Core.Domain.Entities.Task;

public class TaskHistory : Entity<int>
{
    public int TaskId { get; set; }
    public string EventType { get; set; } = null!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public int ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
}
namespace TaskManagementSystem.Core.Dtos.Task;

public class TaskHistoryDto
{
    public int Id { get; set; }
    public string EventType { get; set; } = null!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public int ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
}
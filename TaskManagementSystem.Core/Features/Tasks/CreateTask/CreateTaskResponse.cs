using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.CreateTask;

public record CreateTaskResponse
{
    public TaskDto Task { get; init; } = new();
}
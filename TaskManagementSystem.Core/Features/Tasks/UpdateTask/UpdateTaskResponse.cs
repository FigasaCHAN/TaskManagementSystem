using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.UpdateTask;

public record UpdateTaskResponse
{
    public TaskDto Task { get; init; }
}
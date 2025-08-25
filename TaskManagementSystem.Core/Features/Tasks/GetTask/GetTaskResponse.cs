using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.GetTask;

public record GetTaskResponse
{
    public TaskDto Task { get; init; } = new();
}
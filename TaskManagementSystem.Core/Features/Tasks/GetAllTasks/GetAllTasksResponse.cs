using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.GetAllTasks;

public record GetAllTasksResponse
{
    public IEnumerable<TaskDto> Tasks { get; init; } = [];
}
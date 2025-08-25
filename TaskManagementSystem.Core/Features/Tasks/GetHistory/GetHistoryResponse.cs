using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.GetHistory;

public record GetHistoryResponse
{
    public IEnumerable<TaskHistoryDto> History { get; init; } = [];
}
using MediatR;

namespace TaskManagementSystem.Core.Features.Tasks.GetAllTasks;

public record GetAllTasksRequest : IRequest<GetAllTasksResponse>
{
    public int? Page { get; init; }
    public int? PageSize { get; init; }
}
using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.GetAllTasks;

public class GetAllTasksHandler : IRequestHandler<GetAllTasksRequest, GetAllTasksResponse>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<GetAllTasksResponse> Handle(GetAllTasksRequest request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        return new GetAllTasksResponse { Tasks = _mapper.Map<List<TaskDto>>(tasks) };
    }
}
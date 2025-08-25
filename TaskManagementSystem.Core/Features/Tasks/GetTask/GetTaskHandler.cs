using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.GetTask;

public class GetTaskHandler : IRequestHandler<GetTaskRequest, GetTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public GetTaskHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<GetTaskResponse> Handle(GetTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
        if (task == null)
            throw new KeyNotFoundException($"Task with Id {request.TaskId} not found.");

        return new GetTaskResponse { Task = _mapper.Map<TaskDto>(task) };
    }
}
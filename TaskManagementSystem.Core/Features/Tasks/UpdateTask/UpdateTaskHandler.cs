using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Dtos.Task;
using Task = TaskManagementSystem.Core.Domain.AggregateRoots.Task;
using TaskStatus = TaskManagementSystem.Core.Enums.Task.TaskStatus;

namespace TaskManagementSystem.Core.Features.Tasks.UpdateTask;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskHandler(ITaskRepository taskRepository, IMapper mapper, IMediator mediator)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task == null)
            throw new KeyNotFoundException($"Task with Id {request.Id} not found.");

        var dto = await UpdateTask(task, request.UpdateTaskDto, request.ModifiedBy, cancellationToken);
        return new UpdateTaskResponse { Task = dto };
    }

    private async Task<TaskDto> UpdateTask(Task task, UpdateTaskDto updateTaskDto, int modifiedBy,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(updateTaskDto.Title) || !string.IsNullOrWhiteSpace(updateTaskDto.Description))
            task.UpdateDetails(updateTaskDto.Title ?? task.Title, updateTaskDto.Description ?? task.Description,
                modifiedBy);

        if (updateTaskDto.AssignedUserId.HasValue) task.Assign(updateTaskDto.AssignedUserId.Value, modifiedBy);

        if (!string.IsNullOrWhiteSpace(updateTaskDto.Status) &&
            Enum.TryParse<TaskStatus>(updateTaskDto.Status, out var newStatus) && newStatus != task.Status)
            task.ChangeStatus(Enum.Parse<TaskStatus>(updateTaskDto.Status), modifiedBy);

        await _taskRepository.SaveChanges(cancellationToken);

        await PublishDomainEvent(task, cancellationToken);

        return _mapper.Map<TaskDto>(task);
    }

    private async System.Threading.Tasks.Task PublishDomainEvent(Task task, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in task.PopDomainEvents()) await _mediator.Publish(domainEvent, cancellationToken);
    }
}
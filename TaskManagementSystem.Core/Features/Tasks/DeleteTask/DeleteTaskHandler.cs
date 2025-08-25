using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;

namespace TaskManagementSystem.Core.Features.Tasks.DeleteTask;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskRequest, Unit>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
        if (task == null)
            throw new KeyNotFoundException($"Task with Id {request.TaskId} not found.");

        task.Delete(request.DeletedBy);
        await _taskRepository.SaveChanges(cancellationToken);
        return Unit.Value;
    }
}
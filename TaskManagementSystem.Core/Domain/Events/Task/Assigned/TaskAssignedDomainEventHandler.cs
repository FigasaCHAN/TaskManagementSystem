using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Enums.Task;

namespace TaskManagementSystem.Core.Domain.Events.Task.Assigned;

public class TaskAssignedDomainEventHandler : INotificationHandler<TaskAssignedDomainEvent>
{
    private readonly ITaskRepository _taskRepository;

    public TaskAssignedDomainEventHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async System.Threading.Tasks.Task Handle(TaskAssignedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(notification.Task.Id, cancellationToken);
        if (task == null) return;

        task.AddHistory(
            TaskEventType.Assigned,
            notification.OldAssignedUserId.ToString(),
            notification.NewAssignedUserId.ToString(),
            notification.ChangedBy,
            notification.ChangedAt
        );
        await _taskRepository.SaveChanges(cancellationToken);
    }
}
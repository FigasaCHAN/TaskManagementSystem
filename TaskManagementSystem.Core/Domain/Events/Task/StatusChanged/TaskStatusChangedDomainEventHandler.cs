using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Enums.Task;

namespace TaskManagementSystem.Core.Domain.Events.Task.StatusChanged;

public class TaskStatusChangedDomainEventHandler : INotificationHandler<TaskStatusChangedDomainEvent>
{
    private readonly ITaskRepository _taskRepository;

    public TaskStatusChangedDomainEventHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async System.Threading.Tasks.Task Handle(TaskStatusChangedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(notification.Task.Id, cancellationToken);
        if (task == null) return;

        task.AddHistory(
            TaskEventType.StatusChanged,
            notification.OldStatus.ToString(),
            notification.NewStatus.ToString(),
            notification.ChangedBy,
            notification.ChangedAt
        );
        await _taskRepository.SaveChanges(cancellationToken);
    }
}
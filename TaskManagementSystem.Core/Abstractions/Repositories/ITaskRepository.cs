using TaskManagementSystem.Core.Domain.Entities.Task;
using Task = TaskManagementSystem.Core.Domain.AggregateRoots.Task;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface ITaskRepository : IRepository<Task, int>
{
    Task<IEnumerable<Task>> GetAllAsync(int? page, int? pageSize, CancellationToken cancellationToken = default);
    Task<Task?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Task>> GetByAssignedUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<TaskHistory>> GetTaskHistoryAsync(int taskId, int? page, int? pageSize,
        CancellationToken cancellationToken = default);
}
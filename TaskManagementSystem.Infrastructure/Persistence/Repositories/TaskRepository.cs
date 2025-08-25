using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Domain.Entities.Task;
using TaskManagementSystem.Infrastructure.Extensions;
using Task = TaskManagementSystem.Core.Domain.AggregateRoots.Task;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

public class TaskRepository : Repository<Task, int>, ITaskRepository
{
    public TaskRepository(TaskManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Task>> GetAllAsync(int? page, int? pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page.HasValue && pageSize.HasValue)
            return await AsQueryable()
                .NotDeleted()
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToListAsync(cancellationToken);

        return await AsQueryable()
            .NotDeleted()
            .ToListAsync(cancellationToken);
    }

    public async Task<Task?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await AsQueryable()
            .NotDeleted()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Task>> GetByAssignedUserIdAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        return await AsQueryable()
            .NotDeleted()
            .AssignedTo(userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TaskHistory>> GetTaskHistoryAsync(int taskId, int? page, int? pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page.HasValue && pageSize.HasValue)
            return await AsQueryable()
                .NotDeleted()
                .Where(t => t.Id == taskId)
                .SelectMany(t => t.History)
                .OrderByDescending(h => h.ChangedAt)
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToListAsync(cancellationToken);

        return await AsQueryable()
            .NotDeleted()
            .Where(t => t.Id == taskId)
            .SelectMany(t => t.History)
            .OrderByDescending(h => h.ChangedAt)
            .ToListAsync(cancellationToken);
    }
}
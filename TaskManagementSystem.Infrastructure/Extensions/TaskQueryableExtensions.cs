using Task = TaskManagementSystem.Core.Domain.AggregateRoots.Task;

namespace TaskManagementSystem.Infrastructure.Extensions;

public static class TaskQueryableExtensions
{
    public static IQueryable<Task> NotDeleted(this IQueryable<Task> query)
    {
        return query.Where(t => !t.Deleted);
    }

    public static IQueryable<Task> AssignedTo(this IQueryable<Task> query, int userId)
    {
        return query.Where(t => t.AssignedUserId == userId);
    }
}
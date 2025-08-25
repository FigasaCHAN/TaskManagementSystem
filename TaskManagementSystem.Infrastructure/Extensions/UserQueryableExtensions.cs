using TaskManagementSystem.Core.Domain.AggregateRoots;

namespace TaskManagementSystem.Infrastructure.Extensions;

public static class UserQueryableExtensions
{
    public static IQueryable<User> NotDeleted(this IQueryable<User> query)
    {
        return query.Where(u => !u.Deleted);
    }
}
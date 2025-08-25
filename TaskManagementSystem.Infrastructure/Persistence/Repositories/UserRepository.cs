using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Domain.AggregateRoots;
using TaskManagementSystem.Infrastructure.Extensions;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User, int>, IUserRepository
{
    public UserRepository(TaskManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllAsync(int? page, int? pageSize,
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

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await AsQueryable()
            .NotDeleted()
            .FirstOrDefaultAsync(u => u.Email.Value.ToLower() == email.ToLower(), cancellationToken);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await AsQueryable()
            .NotDeleted()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> Exists(string username, string email, CancellationToken cancellationToken = default)
    {
        return await AsQueryable()
            .AnyAsync(u =>
                u.Username.Value.ToLower() == username.ToLower() ||
                u.Email.Value.ToLower() == email.ToLower(), cancellationToken);
    }
}
using TaskManagementSystem.Core.Domain.AggregateRoots;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IUserRepository : IRepository<User, int>
{
    public Task<IEnumerable<User>> GetAllAsync(int? page, int? pageSize, CancellationToken cancellationToken = default);
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<bool> Exists(string username, string email, CancellationToken cancellationToken = default);
}
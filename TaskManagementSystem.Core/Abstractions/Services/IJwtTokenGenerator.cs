using TaskManagementSystem.Core.Domain.AggregateRoots;

namespace TaskManagementSystem.Core.Abstractions.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
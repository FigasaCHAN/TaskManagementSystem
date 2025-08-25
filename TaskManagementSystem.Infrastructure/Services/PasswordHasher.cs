using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Core.Abstractions.Services;

namespace TaskManagementSystem.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password)
    {
        return _hasher.HashPassword(null, password);
    }

    public bool Verify(string hash, string password)
    {
        var result = _hasher.VerifyHashedPassword(null, hash, password);
        return result == PasswordVerificationResult.Success;
    }
}
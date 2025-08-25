using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Users.CreateUser;

public record CreateUserRequest : IRequest<CreateUserResponse>
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; init; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; init; } = string.Empty;

    [Required] [MinLength(8)] public string Password { get; init; } = string.Empty;
}
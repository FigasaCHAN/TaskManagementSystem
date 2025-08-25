using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Users.DeleteUser;

public record DeleteUserRequest : IRequest<Unit>
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
    public int UserId { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "DeletedBy must be a positive integer.")]
    public int DeletedBy { get; init; }
}
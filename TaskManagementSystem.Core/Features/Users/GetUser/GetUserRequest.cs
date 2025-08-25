using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Users.GetUser;

public record GetUserRequest : IRequest<GetUserResponse>
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
    public int UserId { get; init; }
}
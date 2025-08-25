using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Tasks.CreateTask;

public record CreateTaskRequest : IRequest<CreateTaskResponse>
{
    [Required] public CreateTaskDto CreateTaskDto { get; init; } = new();

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CreatedBy must be a positive integer.")]
    public int CreatedBy { get; init; }
}
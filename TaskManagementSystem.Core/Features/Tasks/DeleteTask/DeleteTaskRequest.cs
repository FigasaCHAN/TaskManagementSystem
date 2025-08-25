using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Tasks.DeleteTask;

public record DeleteTaskRequest : IRequest<Unit>
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
    public int TaskId { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "DeletedBy must be a positive integer.")]
    public int DeletedBy { get; init; }
}
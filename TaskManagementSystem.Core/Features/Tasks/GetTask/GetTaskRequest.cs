using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Tasks.GetTask;

public record GetTaskRequest : IRequest<GetTaskResponse>
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "TaskId must be a positive integer.")]
    public int TaskId { get; init; }
}
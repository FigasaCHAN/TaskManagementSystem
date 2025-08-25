using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Tasks.UpdateTask;

public class UpdateTaskRequest : IRequest<UpdateTaskResponse>
{
    public int Id { get; set; }

    [Required] public UpdateTaskDto UpdateTaskDto { get; init; } = new();

    [Required] public int ModifiedBy { get; init; }
}
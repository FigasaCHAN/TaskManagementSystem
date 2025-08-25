using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.Features.Tasks.CreateTask;
using TaskManagementSystem.Core.Features.Tasks.DeleteTask;
using TaskManagementSystem.Core.Features.Tasks.GetAllTasks;
using TaskManagementSystem.Core.Features.Tasks.GetHistory;
using TaskManagementSystem.Core.Features.Tasks.GetTask;
using TaskManagementSystem.Core.Features.Tasks.UpdateTask;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize] Para pruebas se desactiva la autorizacion (se debe de configurar swagger para enviar el token por header o utilizar postman)
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto createTaskDto)
    {
        var response = await _mediator.Send(new CreateTaskRequest { CreateTaskDto = createTaskDto, CreatedBy = 1 });
        return CreatedAtAction(nameof(GetById), new { id = response.Task.Id }, response.Task);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetTaskRequest { TaskId = id });
        return Ok(response.Task);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var response = await _mediator.Send(new GetAllTasksRequest
        {
            Page = pageNumber,
            PageSize = pageSize
        });
        return Ok(response.Tasks);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto request)
    {
        var response = await _mediator.Send(new UpdateTaskRequest { Id = id, UpdateTaskDto = request, ModifiedBy = 1 });
        return Ok(response.Task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteTaskRequest { TaskId = id, DeletedBy = 1 });
        return NoContent();
    }

    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetHistory(int id)
    {
        var response = await _mediator.Send(new GetHistoryRequest { TaskId = id });
        return Ok(response.History);
    }
}
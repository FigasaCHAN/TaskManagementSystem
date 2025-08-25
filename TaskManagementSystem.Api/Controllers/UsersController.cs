using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.Features.Users.CreateUser;
using TaskManagementSystem.Core.Features.Users.DeleteUser;
using TaskManagementSystem.Core.Features.Users.GetAllUsers;
using TaskManagementSystem.Core.Features.Users.GetUser;
using TaskManagementSystem.Core.Features.Users.UpdateUser;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize] Para pruebas se desactiva la autorizacion (se debe de configurar swagger para enviar el token por header o utilizar postman)
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.User.Id }, response.User);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetUserRequest { UserId = id });
        return Ok(response.User);
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> GetAll(int? pageNumber, int? pageSize)
    {
        var response = await _mediator.Send(new GetAllUsersRequest
        {
            Page = pageNumber,
            PageSize = pageSize
        });
        return Ok(response.Users);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteUserRequest { UserId = id, DeletedBy = 1 });
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto request)
    {
        var response = await _mediator.Send(new UpdateUserRequest { Id = id, UpdateUserDto = request, ModifiedBy = 1 });
        return Ok(response.User);
    }
}
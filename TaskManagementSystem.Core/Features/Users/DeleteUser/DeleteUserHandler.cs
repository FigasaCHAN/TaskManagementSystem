using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;

namespace TaskManagementSystem.Core.Features.Users.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, Unit>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository, ITaskRepository taskRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
    }


    public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null) throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

        await DeleteTaskAssigned(user.Id, request.DeletedBy, cancellationToken);

        user.Delete();
        await _userRepository.SaveChanges(cancellationToken);

        return Unit.Value;
    }

    private async Task DeleteTaskAssigned(int userId, int deletedBy, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetByAssignedUserIdAsync(userId, cancellationToken);
        foreach (var task in tasks) task.Delete(deletedBy);
        await _taskRepository.SaveChanges(cancellationToken);
    }
}
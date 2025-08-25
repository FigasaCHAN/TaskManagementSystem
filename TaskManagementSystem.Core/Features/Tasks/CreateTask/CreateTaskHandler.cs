using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Dtos.Task;
using Task = TaskManagementSystem.Core.Domain.AggregateRoots.Task;

namespace TaskManagementSystem.Core.Features.Tasks.CreateTask;

public class CreateTaskHandler : IRequestHandler<CreateTaskRequest, CreateTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public CreateTaskHandler(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateTaskResponse> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        await Validate(request);

        var task = CreateTaskFromRequest(request);
        await _taskRepository.Add(task, cancellationToken);
        await _taskRepository.SaveChanges(cancellationToken);

        return new CreateTaskResponse { Task = _mapper.Map<TaskDto>(task) };
    }

    private async System.Threading.Tasks.Task Validate(CreateTaskRequest request)
    {
        await ValidateUserActive(request.CreateTaskDto);
    }

    private async System.Threading.Tasks.Task ValidateUserActive(CreateTaskDto createTaskDto)
    {
        var user = await _userRepository.GetByIdAsync(createTaskDto.AssignedUserId);
        if (user == null) throw new NullReferenceException($"User with ID {createTaskDto.AssignedUserId} not found.");
    }

    private Task CreateTaskFromRequest(CreateTaskRequest request)
    {
        var createTaskDto = request.CreateTaskDto;
        return new Task(
            createTaskDto.Title,
            createTaskDto.Description,
            createTaskDto.AssignedUserId,
            request.CreatedBy
        );
    }
}
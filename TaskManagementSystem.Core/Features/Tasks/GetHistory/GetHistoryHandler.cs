using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Dtos.Task;

namespace TaskManagementSystem.Core.Features.Tasks.GetHistory;

public class GetHistoryHandler : IRequestHandler<GetHistoryRequest, GetHistoryResponse>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public GetHistoryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<GetHistoryResponse> Handle(GetHistoryRequest request, CancellationToken cancellationToken)
    {
        var history = await _taskRepository.GetTaskHistoryAsync(request.TaskId, request.PageNumber, request.PageSize,
            cancellationToken);
        return new GetHistoryResponse { History = _mapper.Map<IEnumerable<TaskHistoryDto>>(history) };
    }
}
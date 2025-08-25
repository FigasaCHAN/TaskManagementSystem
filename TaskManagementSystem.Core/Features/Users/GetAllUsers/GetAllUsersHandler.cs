using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Features.Users.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        return new GetAllUsersResponse
        {
            Users = _mapper.Map<IEnumerable<UserDto>>(users),
            TotalCount = users.Count()
        };
    }
}
using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Features.Users.GetUser;

public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null) throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

        return new GetUserResponse
        {
            User = _mapper.Map<UserDto>(user)
        };
    }
}
using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Abstractions.Services;
using TaskManagementSystem.Core.Domain.AggregateRoots;
using TaskManagementSystem.Core.Domain.ValueObjects;
using TaskManagementSystem.Core.Domain.ValueObjects.Common;
using TaskManagementSystem.Core.Dtos.User;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementSystem.Core.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        await Validate(request);
        var user = CreateUserFromRequest(request);
        await _userRepository.Add(user, cancellationToken);
        await _userRepository.SaveChanges(cancellationToken);

        return new CreateUserResponse
        {
            User = _mapper.Map<UserDto>(user)
        };
    }

    private async Task Validate(CreateUserRequest request)
    {
        await ValidateUserExistence(request);
    }

    private async Task ValidateUserExistence(CreateUserRequest request)
    {
        if (await _userRepository.Exists(request.Username, request.Email))
            throw new ArgumentException("User with the same username or email already exists.");
    }

    private User CreateUserFromRequest(CreateUserRequest request)
    {
        var userName = Username.Create(request.Username);
        var email = Email.Create(request.Email);
        var password = Password.Create(_passwordHasher.Hash(request.Password));
        return new User(userName, email, password);
    }
}
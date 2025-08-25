using AutoMapper;
using MediatR;
using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Abstractions.Services;
using TaskManagementSystem.Core.Domain.ValueObjects;
using TaskManagementSystem.Core.Domain.ValueObjects.Common;
using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Features.Users.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken) ??
                   throw new KeyNotFoundException($"User with ID {request.Id} not found.");
        var updateUserDto = request.UpdateUserDto;

        user.Update(
            updateUserDto.Username != null ? Username.Create(updateUserDto.Username) : user.Username,
            updateUserDto.Email != null ? Email.Create(updateUserDto.Email) : null,
            updateUserDto.Password != null ? Password.Create(_passwordHasher.Hash(updateUserDto.Password)) : null);

        await _userRepository.SaveChanges(cancellationToken);

        return new UpdateUserResponse
        {
            User = _mapper.Map<UserDto>(user)
        };
    }
}
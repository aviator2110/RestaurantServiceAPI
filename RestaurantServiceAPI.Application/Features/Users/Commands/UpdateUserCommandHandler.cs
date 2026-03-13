using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await this._userRepository.GetUserByIdAsync(request.Id);

        if (user is null)
            throw new Exception("User with this Id doesn't exist!");

        user.UpdateProfile(
            request.UpdateRequest.FirstName,
            request.UpdateRequest.LastName,
            request.UpdateRequest.Email);

        await this._userRepository.UpdateAsync(user);

        return this._mapper.Map<UserResponseDto>(user);
    }
}
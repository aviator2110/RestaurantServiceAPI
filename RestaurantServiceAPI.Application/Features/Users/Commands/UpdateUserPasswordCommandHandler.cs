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

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await this._userRepository.GetUserByIdAsync(request.Id);

        if (user is null)
            throw new Exception("User with this Id doesn't exist!");

        var isEqual = await this._userRepository.CheckPasswordAsync(user.Email, request.Password);

        if (isEqual)
            throw new Exception("Passwords are same");


    }
}

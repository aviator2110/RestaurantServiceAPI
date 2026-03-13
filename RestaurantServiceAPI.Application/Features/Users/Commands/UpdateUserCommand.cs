using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Users.Commands;

public record UpdateUserCommand(Guid Id, UpdateUserRequestDto UpdateRequest) : IRequest<UserResponseDto>;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Waiters.Commands;

public record UpdateWaiterPinCommand(Guid Id, string NewPin) : IRequest<WaiterResponseDto>;
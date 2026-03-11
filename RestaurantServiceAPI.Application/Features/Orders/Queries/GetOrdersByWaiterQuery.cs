using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Orders.Queries;

public record GetOrdersByWaiterQuery(Guid WaiterId) : IRequest<IEnumerable<OrderResponseDto>>;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.OrderItems.Commands;

public record UpdateOrderItemStatusCommand(Guid Id, string Status) : IRequest<OrderItemResponseDto>;
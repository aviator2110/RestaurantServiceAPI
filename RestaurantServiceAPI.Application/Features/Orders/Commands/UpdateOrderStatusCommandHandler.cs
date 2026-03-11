using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Orders.Commands;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, OrderResponseDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<OrderResponseDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await this._orderRepository.GetByIdAsync(request.Id);

        if (order is null)
            throw new Exception("Order not found");

        order.Status = Enum.Parse<OrderStatus>(request.updateStatusRequest.Status);

        await this._orderRepository.UpdateAsync(order);

        return this._mapper.Map<OrderResponseDto>(order);
    }
}
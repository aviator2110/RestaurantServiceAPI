using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Orders.Commands;

public class UpdateOrderItemQuantityCommandHandler : IRequestHandler<UpdateOrderItemQuantityCommand, OrderResponseDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public UpdateOrderItemQuantityCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<OrderResponseDto> Handle(UpdateOrderItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var order = await this._orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        order.UpdateItemQuantity(request.OrderItemId, request.Quantity);

        await this._orderRepository.UpdateAsync(order);

        return this._mapper.Map<OrderResponseDto>(order);
    }
}

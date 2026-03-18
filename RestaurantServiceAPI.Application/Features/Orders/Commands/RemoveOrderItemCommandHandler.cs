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

public class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand, OrderResponseDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IOrderItemRepository _orderItemRepository;

    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository, IMapper mapper, IOrderItemRepository orderItemRepository)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
        this._orderItemRepository = orderItemRepository;
    }

    public async Task<OrderResponseDto> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await this._orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        order.RemoveItem(request.OrderItemId);

        var isSaved = await this._orderItemRepository.SaveChangesAsync();

        if (!isSaved)
            throw new Exception("Order item not canceled for some reasons!");

        await this._orderRepository.UpdateAsync(order);

        return this._mapper.Map<OrderResponseDto>(order);
    }
}

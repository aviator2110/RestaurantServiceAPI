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

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, OrderResponseDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CancelOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<OrderResponseDto> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await this._orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        order.Cancel();

        await this._orderRepository.UpdateAsync(order);

        return this._mapper.Map<OrderResponseDto>(order);
    }
}

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

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, OrderResponseDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CompleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<OrderResponseDto> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);

        if (order is null)
            throw new Exception("Order not found");

        order.Complete();

        await _orderRepository.UpdateAsync(order);

        return _mapper.Map<OrderResponseDto>(order);
    }
}

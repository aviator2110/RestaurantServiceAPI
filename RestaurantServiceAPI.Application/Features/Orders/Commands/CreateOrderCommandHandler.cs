using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponseDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<OrderResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.createRequest.TableId, request.createRequest.WaiterId);

        await _orderRepository.CreateAsync(order);

        return _mapper.Map<OrderResponseDto>(order);
    }
}

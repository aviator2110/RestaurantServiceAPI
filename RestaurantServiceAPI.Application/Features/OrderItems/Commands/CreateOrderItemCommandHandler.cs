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

namespace RestaurantServiceAPI.Application.Features.OrderItems.Commands;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, OrderItemResponseDto>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        this._orderItemRepository = orderItemRepository;
        this._mapper = mapper;
    }

    public async Task<OrderItemResponseDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = new OrderItem(
                                    request.request.ProductId,
                                    request.request.OrderId,
                                    request.request.Quantity,
                                    request.request.UnitPrice
                                    );

        await this._orderItemRepository.CreateAsync(orderItem);

        return this._mapper.Map<OrderItemResponseDto>(orderItem);
    }
}

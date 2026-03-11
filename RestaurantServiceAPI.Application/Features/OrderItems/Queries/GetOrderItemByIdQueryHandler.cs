using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.OrderItems.Queries;

public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, OrderItemResponseDto?>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public GetOrderItemByIdQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        this._orderItemRepository = orderItemRepository;
        this._mapper = mapper;
    }

    public async Task<OrderItemResponseDto?> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        var orderItem = await this._orderItemRepository.GetByIdAsync(request.Id);

        if (orderItem is null)
            return null;

        return this._mapper.Map<OrderItemResponseDto>(orderItem);
    }
}
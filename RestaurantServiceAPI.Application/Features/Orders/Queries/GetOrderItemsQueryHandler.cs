using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Orders.Queries;

public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, IEnumerable<OrderItemResponseDto>>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public GetOrderItemsQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        this._orderItemRepository = orderItemRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemResponseDto>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var orderItems = await this._orderItemRepository.GetByOrderIdAsync(request.OrderId);

        return this._mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItems);
    }
}
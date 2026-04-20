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

public class GetPendingItemsQueryHandler : IRequestHandler<GetPendingItemsQuery, IEnumerable<OrderItemResponseDto>>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public GetPendingItemsQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        this._orderItemRepository = orderItemRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemResponseDto>> Handle(GetPendingItemsQuery request, CancellationToken cancellationToken)
    {
        var orderItems = await this._orderItemRepository.GetPendingItemsAsync();

        return this._mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItems);
    }
}

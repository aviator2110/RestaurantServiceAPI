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

internal class GetActiveOrdersQueryHandler : IRequestHandler<GetActiveOrdersQuery, IEnumerable<OrderResponseDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetActiveOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponseDto>> Handle(GetActiveOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await this._orderRepository.GetActiveOrdersAsync();

        return this._mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }
}

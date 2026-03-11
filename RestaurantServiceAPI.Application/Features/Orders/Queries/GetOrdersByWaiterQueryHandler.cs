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

public class GetOrdersByWaiterQueryHandler : IRequestHandler<GetOrdersByWaiterQuery, IEnumerable<OrderResponseDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByWaiterQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponseDto>> Handle(GetOrdersByWaiterQuery request, CancellationToken cancellationToken)
    {
        var orders = await this._orderRepository.GetByWaiterIdAsync(request.WaiterId);

        return this._mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }
}
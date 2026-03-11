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

public class GetOrdersByTableQueryHandler : IRequestHandler<GetOrdersByTableQuery, OrderResponseDto?>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByTableQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<OrderResponseDto?> Handle(GetOrdersByTableQuery request, CancellationToken cancellationToken)
    {
        var order = await this._orderRepository.GetByTableIdAsync(request.TableId);

        if (order == null)
            return null;

        return this._mapper.Map<OrderResponseDto>(order);
    }
}

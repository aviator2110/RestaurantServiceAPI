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

public class GetOrdersByTableQueryHandler : IRequestHandler<GetOrdersByTableQuery, IEnumerable<OrderResponseDto?>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByTableQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponseDto?>> Handle(GetOrdersByTableQuery request, CancellationToken cancellationToken)
    {
        var orders = await this._orderRepository.GetByTableIdAsync(request.TableId);

        return this._mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }
}

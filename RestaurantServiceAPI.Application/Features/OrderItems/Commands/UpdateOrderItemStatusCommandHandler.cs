using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.OrderItems.Commands;

public class UpdateOrderItemStatusCommandHandler : IRequestHandler<UpdateOrderItemStatusCommand, OrderItemResponseDto>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public UpdateOrderItemStatusCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        this._orderItemRepository = orderItemRepository;
        this._mapper = mapper;
    }

    public async Task<OrderItemResponseDto> Handle(UpdateOrderItemStatusCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await this._orderItemRepository.GetByIdAsync(request.Id);

        if (orderItem is null)
            throw new Exception("Order item not found");

        orderItem.Status = Enum.Parse<OrderItemStatus>(request.Status);

        await this._orderItemRepository.UpdateAsync(orderItem);

        return this._mapper.Map<OrderItemResponseDto>(orderItem);
    }
}
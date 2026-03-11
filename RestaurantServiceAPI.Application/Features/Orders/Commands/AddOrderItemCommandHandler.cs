using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Orders.Commands;

public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, OrderResponseDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public AddOrderItemCommandHandler(
        IOrderRepository orderRepository, 
        IOrderItemRepository orderItemRepository, 
        IProductRepository productRepository, 
        IMapper mapper)
    {
        this._orderRepository = orderRepository;
        this._orderItemRepository = orderItemRepository;
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<OrderResponseDto> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await this._orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        var product = await this._productRepository.GetByIdAsync(request.ProductId);

        if (product is null || product.IsAvailable is false)
            throw new Exception("Product not available");

        order.AddItem(product, request.Quantity);

        await this._orderRepository.UpdateAsync(order);

        return this._mapper.Map<OrderResponseDto>(order);
    }
}

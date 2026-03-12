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

namespace RestaurantServiceAPI.Application.Features.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.createRequest.Name,
            request.createRequest.Description,
            request.createRequest.Price,
            Enum.Parse<MenuCategory>(request.createRequest.Category),
            request.createRequest.IsAvailable);

        await this._productRepository.CreateAsync(product);

        return this._mapper.Map<ProductResponseDto>(product);
    }
}

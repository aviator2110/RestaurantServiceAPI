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

namespace RestaurantServiceAPI.Application.Features.Products.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDto?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<ProductResponseDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this._productRepository.GetByIdAsync(request.Id);

        if (product is null)
            return null;

        if (!string.IsNullOrEmpty(request.UpdateRequest.Name) && !string.IsNullOrEmpty(request.UpdateRequest.Description))
            product.UpdateDetails(request.UpdateRequest.Name, request.UpdateRequest.Description);

        if (request.UpdateRequest.Price > 0)
            product.ChangePrice(request.UpdateRequest.Price);

        if (!string.IsNullOrEmpty(request.UpdateRequest.Category))
            product.ChangeCategory(Enum.Parse<MenuCategory>(request.UpdateRequest.Category));

        if (request.UpdateRequest.IsAvailable)
            product.Activate();
        else 
            product.Deactivate();

        return this._mapper.Map<ProductResponseDto>(product);
    }
}

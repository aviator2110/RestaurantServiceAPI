using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Products.Commands;

public class DeactivateProductCommandHandler : IRequestHandler<DeactivateProductCommand, ProductResponseDto?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public DeactivateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<ProductResponseDto?> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this._productRepository.GetByIdAsync(request.Id);

        if (product is null)
            return null;

        if (!product.IsAvailable)
            return this._mapper.Map<ProductResponseDto>(product);

        product.Deactivate();

        var isSaved = await this._productRepository.SaveChangesAsync();

        if (!isSaved)
            throw new Exception("Product not canceled for some reasons!");

        return this._mapper.Map<ProductResponseDto>(product);
    }
}

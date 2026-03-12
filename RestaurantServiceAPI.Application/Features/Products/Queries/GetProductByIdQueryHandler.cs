using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Products.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<ProductResponseDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await this._productRepository.GetByIdAsync(request.Id);

        if (product is null)
            return null;

        return this._mapper.Map<ProductResponseDto>(product);
    }
}
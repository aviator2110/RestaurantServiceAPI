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

public class GetAvailableProductsQueryHandler : IRequestHandler<GetAvailableProductsQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAvailableProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(GetAvailableProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await this._productRepository.GetAvailableAsync();

        return this._mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
}
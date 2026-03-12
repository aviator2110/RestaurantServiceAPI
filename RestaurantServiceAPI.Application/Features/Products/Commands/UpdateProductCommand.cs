using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Products.Commands;

public record UpdateProductCommand(UpdateProductRequestDto UpdateRequest, Guid Id) : IRequest<ProductResponseDto?>;
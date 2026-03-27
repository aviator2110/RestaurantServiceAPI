using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantServiceAPI.Application.Common;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Features.Products.Commands;
using RestaurantServiceAPI.Application.Features.Products.Queries;

namespace RestaurantServiceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Get all products.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductResponseDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetProductsQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<ProductResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get available products.
    /// </summary>
    [HttpGet("available")]
    [Authorize(Roles = "Admin,Waiter,Cook,Bartender")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductResponseDto>>>> GetAvailable(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetAvailableProductsQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<ProductResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get product by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<ProductResponseDto>.ErrorResponse($"Product with id '{id}' was not found."));

        return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(result));
    }

    /// <summary>
    /// Create a new product.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Create(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            ApiResponse<ProductResponseDto>.SuccessResponse(result, "Product created successfully."));
    }

    /// <summary>
    /// Update product.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Update(
        Guid id,
        [FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(ApiResponse<ProductResponseDto>.ErrorResponse("Route id does not match command id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<ProductResponseDto>.ErrorResponse($"Product with id '{id}' was not found."));

        return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(result, "Product updated successfully."));
    }

    /// <summary>
    /// Deactivate product.
    /// </summary>
    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new DeactivateProductCommand(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<ProductResponseDto>.ErrorResponse($"Product with id '{id}' was not found."));

        return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(result, "Product deactivated successfully."));
    }
}
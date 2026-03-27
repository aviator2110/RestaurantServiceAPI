using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantServiceAPI.Application.Common;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Features.OrderItems.Commands;
using RestaurantServiceAPI.Application.Features.OrderItems.Queries;

namespace RestaurantServiceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Get order item by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin,Waiter,Cook,Bartender")]
    public async Task<ActionResult<ApiResponse<OrderItemResponseDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetOrderItemByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderItemResponseDto>.ErrorResponse($"Order item with id '{id}' was not found."));

        return Ok(ApiResponse<OrderItemResponseDto>.SuccessResponse(result));
    }

    /// <summary>
    /// Update order item status.
    /// </summary>
    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Admin,Cook,Bartender")]
    public async Task<ActionResult<ApiResponse<OrderItemResponseDto>>> UpdateStatus(
        Guid id,
        [FromBody] UpdateOrderItemStatusCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(ApiResponse<OrderItemResponseDto>.ErrorResponse("Route id does not match command id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderItemResponseDto>.ErrorResponse($"Order item with id '{id}' was not found."));

        return Ok(ApiResponse<OrderItemResponseDto>.SuccessResponse(result, "Order item status updated successfully."));
    }
}
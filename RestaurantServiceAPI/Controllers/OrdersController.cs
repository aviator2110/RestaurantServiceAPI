using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantServiceAPI.Application.Common;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Features.Orders.Commands;
using RestaurantServiceAPI.Application.Features.Orders.Queries;

namespace RestaurantServiceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Get all orders.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderResponseDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetOrdersQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get active orders.
    /// </summary>
    [HttpGet("active")]
    [Authorize(Roles = "Admin,Waiter,Cook,Bartender")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderResponseDto>>>> GetActive(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetActiveOrdersQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get order by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin,Waiter,Cook,Bartender")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetOrderByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse($"Order with id '{id}' was not found."));

        return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(result));
    }

    /// <summary>
    /// Get all order items by order id.
    /// </summary>
    [HttpGet("{id:guid}/items")]
    [Authorize(Roles = "Admin,Waiter,Cook,Bartender")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderItemResponseDto>>>> GetItems(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetOrderItemsQuery(id), cancellationToken);

        return Ok(ApiResponse<IEnumerable<OrderItemResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get orders by table id.
    /// </summary>
    [HttpGet("by-table/{tableId:guid}")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderResponseDto>>>> GetByTable(Guid tableId, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetOrdersByTableQuery(tableId), cancellationToken);

        return Ok(ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(result!));
    }

    /// <summary>
    /// Get orders by waiter id.
    /// </summary>
    [HttpGet("by-waiter/{waiterId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderResponseDto>>>> GetByWaiter(Guid waiterId, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetOrdersByWaiterQuery(waiterId), cancellationToken);

        return Ok(ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Create a new order.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Create(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            ApiResponse<OrderResponseDto>.SuccessResponse(result, "Order created successfully."));
    }

    /// <summary>
    /// Add order item.
    /// </summary>
    [HttpPost("{id:guid}/items")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> AddOrderItem(
        Guid id,
        [FromBody] AddOrderItemCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.OrderId)
            return BadRequest(ApiResponse<OrderResponseDto>.ErrorResponse("Route id does not match command order id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse($"Order with id '{id}' was not found."));

        return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(result, "Order item added successfully."));
    }

    /// <summary>
    /// Remove order item.
    /// </summary>
    [HttpDelete("{id:guid}/items/{orderItemId:guid}")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> RemoveOrderItem(
        Guid id,
        Guid orderItemId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveOrderItemCommand(id, orderItemId);

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse($"Order with id '{id}' or order item with id '{orderItemId}' was not found."));

        return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(result, "Order item removed successfully."));
    }

    /// <summary>
    /// Update order item quantity.
    /// </summary>
    [HttpPatch("{id:guid}/items/{orderItemId:guid}/quantity")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> UpdateOrderItemQuantity(
        Guid id,
        Guid orderItemId,
        [FromBody] UpdateOrderItemQuantityCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.OrderId)
            return BadRequest(ApiResponse<OrderResponseDto>.ErrorResponse("Route id does not match command order id."));

        if (orderItemId != command.OrderItemId)
            return BadRequest(ApiResponse<OrderResponseDto>.ErrorResponse("Route order item id does not match command order item id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse($"Order with id '{id}' or order item with id '{orderItemId}' was not found."));

        return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(result, "Order item quantity updated successfully."));
    }

    /// <summary>
    /// Update order status.
    /// </summary>
    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> UpdateStatus(
        Guid id,
        [FromBody] UpdateOrderStatusCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(ApiResponse<OrderResponseDto>.ErrorResponse("Route id does not match command id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse($"Order with id '{id}' was not found."));

        return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(result, "Order status updated successfully."));
    }

    /// <summary>
    /// Complete order.
    /// </summary>
    [HttpPatch("{id:guid}/complete")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Complete(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new CompleteOrderCommand(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse($"Order with id '{id}' was not found."));

        return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(result, "Order completed successfully."));
    }

    /// <summary>
    /// Cancel order.
    /// </summary>
    [HttpPatch("{id:guid}/cancel")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new CancelOrderCommand(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse($"Order with id '{id}' was not found."));

        return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(result, "Order canceled successfully."));
    }
}

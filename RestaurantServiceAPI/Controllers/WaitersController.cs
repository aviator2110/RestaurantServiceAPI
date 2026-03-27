using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantServiceAPI.Application.Common;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Features.Waiters.Commands;
using RestaurantServiceAPI.Application.Features.Waiters.Queries;

namespace RestaurantServiceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WaitersController : ControllerBase
{
    private readonly IMediator _mediator;

    public WaitersController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Get all waiters.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<IEnumerable<WaiterResponseDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetWaitersQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<WaiterResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get active waiters.
    /// </summary>
    [HttpGet("active")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<IEnumerable<WaiterResponseDto>>>> GetActive(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetActiveWaitersQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<WaiterResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get waiter by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<WaiterResponseDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetWaiterByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<WaiterResponseDto>.ErrorResponse($"Waiter with id '{id}' was not found."));

        return Ok(ApiResponse<WaiterResponseDto>.SuccessResponse(result));
    }

    /// <summary>
    /// Create a new waiter.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<WaiterResponseDto>>> Create(
        [FromBody] CreateWaiterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            ApiResponse<WaiterResponseDto>.SuccessResponse(result, "Waiter created successfully."));
    }

    /// <summary>
    /// Update waiter details.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<WaiterResponseDto>>> UpdateDetails(
        Guid id,
        [FromBody] UpdateWaiterDetailsCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(ApiResponse<WaiterResponseDto>.ErrorResponse("Route id does not match command id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<WaiterResponseDto>.ErrorResponse($"Waiter with id '{id}' was not found."));

        return Ok(ApiResponse<WaiterResponseDto>.SuccessResponse(result, "Waiter details updated successfully."));
    }

    /// <summary>
    /// Update waiter pin.
    /// </summary>
    [HttpPatch("{id:guid}/pin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<WaiterResponseDto>>> UpdatePin(
        Guid id,
        [FromBody] UpdateWaiterPinCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(ApiResponse<WaiterResponseDto>.ErrorResponse("Route id does not match command id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<WaiterResponseDto>.ErrorResponse($"Waiter with id '{id}' was not found."));

        return Ok(ApiResponse<WaiterResponseDto>.SuccessResponse(result, "Waiter pin updated successfully."));
    }

    /// <summary>
    /// Deactivate waiter.
    /// </summary>
    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<WaiterResponseDto>>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new DeactivateWaiterCommand(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<WaiterResponseDto>.ErrorResponse($"Waiter with id '{id}' was not found."));

        return Ok(ApiResponse<WaiterResponseDto>.SuccessResponse(result, "Waiter deactivated successfully."));
    }
}
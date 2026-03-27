using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantServiceAPI.Application.Common;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Features.Tables.Commands;
using RestaurantServiceAPI.Application.Features.Tables.Queries;

namespace RestaurantServiceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TablesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TablesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Get all tables.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TableResponseDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetTablesQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<TableResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get active tables.
    /// </summary>
    [HttpGet("active")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TableResponseDto>>>> GetActive(CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetActiveTablesQuery(), cancellationToken);

        return Ok(ApiResponse<IEnumerable<TableResponseDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Get table by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin,Waiter")]
    public async Task<ActionResult<ApiResponse<TableResponseDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new GetTableByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<TableResponseDto>.ErrorResponse($"Table with id '{id}' was not found."));

        return Ok(ApiResponse<TableResponseDto>.SuccessResponse(result));
    }

    /// <summary>
    /// Create a new table.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<TableResponseDto>>> Create(
        [FromBody] CreateTableCommand command,
        CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            ApiResponse<TableResponseDto>.SuccessResponse(result, "Table created successfully."));
    }

    /// <summary>
    /// Update table.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<TableResponseDto>>> Update(
        Guid id,
        [FromBody] UpdateTableCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(ApiResponse<TableResponseDto>.ErrorResponse("Route id does not match command id."));

        var result = await this._mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<TableResponseDto>.ErrorResponse($"Table with id '{id}' was not found."));

        return Ok(ApiResponse<TableResponseDto>.SuccessResponse(result, "Table updated successfully."));
    }

    /// <summary>
    /// Deactivate table.
    /// </summary>
    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<TableResponseDto>>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(new DeactiveTableCommand(id), cancellationToken);

        if (result is null)
            return NotFound(ApiResponse<TableResponseDto>.ErrorResponse($"Table with id '{id}' was not found."));

        return Ok(ApiResponse<TableResponseDto>.SuccessResponse(result, "Table deactivated successfully."));
    }
}
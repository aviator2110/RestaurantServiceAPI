using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.DTOs;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public int TableNumber { get; set; }
    public Guid WaiterId { get; set; }
    public string WaiterName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<OrderItemResponseDto> Items { get; set; } = new List<OrderItemResponseDto>();
}

public class CreateOrderRequestDto
{
    public Guid TableId { get; set; }
}

public class UpdateOrderStatusRequestDto
{
    public string Status { get; set; } = string.Empty;
}
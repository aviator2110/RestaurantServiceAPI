using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid TableId { get; set; }
    public Guid WaiterId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTimeOffset StartedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset CompletedAt { get; set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalAmount => _items.Sum(x => x.TotalPrice);
}
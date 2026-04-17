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
    public DateTimeOffset? CompletedAt { get; set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalAmount => _items.Where(x => x.Status != OrderItemStatus.Cancelled).Sum(x => x.TotalPrice);

    public Table Table { get; set; } = null!;
    public Waiter Waiter { get; set; } = null!;

    private Order() { }

    public Order(Guid tableId, Guid waiterId)
    {
        Id = Guid.NewGuid();
        TableId = tableId;
        WaiterId = waiterId;
        Status = OrderStatus.Created;
        StartedAt = DateTimeOffset.UtcNow;
    }

    public void AddItem(Product product, Guid orderId, int quantity)
    {
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException("Cannot modify completed order");

        var item = new OrderItem(product.Id, orderId, quantity, product.Price);

        _items.Add(item);
    }

    public void RemoveItem(Guid orderItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == orderItemId);

        if (item is null)
            throw new Exception("Item not found");

        _items.Remove(item);
    }

    public void Complete()
    {
        if (!_items.Any())
            throw new Exception("Order must contain items");

        Status = OrderStatus.Completed;
        CompletedAt = DateTimeOffset.UtcNow;
    }

    public void Cancel()
    {
        if (_items.Any(x => x.Status == OrderItemStatus.Ready))
            throw new InvalidOperationException("Order contains ready items");

        Status = OrderStatus.Cancelled;

        foreach (var item in _items)
        {
            item.Cancel();
        }
    }

    public void UpdateItemQuantity(Guid orderItemId, int quantity)
    {
        if (quantity <= 0)
            throw new Exception("Quantity must be greater than zero");

        var item = _items.FirstOrDefault(x => x.Id == orderItemId);

        if (item is null)
            throw new Exception("Order item not found");

        item.UpdateQuantity(quantity);
    }
}
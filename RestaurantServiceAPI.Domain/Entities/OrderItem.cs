using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public OrderItemStatus Status { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;

    private OrderItem() { }

    public OrderItem(Guid productId, int quantity, decimal price)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = price;
        Status = OrderItemStatus.Pending;
    }

    public void Ready()
    {
        Status = OrderItemStatus.Ready;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new Exception("Quantity must be greater than zero");

        Quantity = quantity;
    }

    public void Cancel()
    {
        if (Status == OrderItemStatus.Ready)
            throw new InvalidOperationException("Ready item cannot be cancelled");

        Status = OrderItemStatus.Cancelled;
    }
}
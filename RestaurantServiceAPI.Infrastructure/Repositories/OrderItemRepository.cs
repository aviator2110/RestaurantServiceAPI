using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using RestaurantServiceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly RestaurantServiceDbContext _context;

    public OrderItemRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    public async Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        await this._context.OrderItems.AddAsync(orderItem);

        await this._context.SaveChangesAsync();

        return orderItem;
    }

    public async Task DeleteAsync(Guid id)
    {
        var orderItem = await this.GetByIdAsync(id);

        if (orderItem is null)
            return;

        orderItem.Status = OrderItemStatus.Cancelled;

        await this._context.SaveChangesAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(Guid id)
    {
        var orderItemsQuery = this._context.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .AsQueryable();

        var orderItem = await orderItemsQuery
            .FirstOrDefaultAsync(oi => oi.Id == id);

        if (orderItem is null)
            return null;

        return orderItem;
    }

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId)
    {
        var orderItemsQuery = this._context.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .AsQueryable();

        var orderItems = await orderItemsQuery
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();

        return orderItems;
    }

    public async Task<IEnumerable<OrderItem>> GetPendingItemsAsync()
    {
        var orderItemsQuery = this._context.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .AsQueryable();

        var orderItems = await orderItemsQuery
            .Where(oi => oi.Status == OrderItemStatus.Pending || oi.Status == OrderItemStatus.Preparing)
            .ToListAsync();

        return orderItems;
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        this._context.OrderItems.Update(orderItem);

        await this._context.SaveChangesAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

public class OrderRepository : IOrderRepository
{
    private readonly RestaurantServiceDbContext _context;

    public OrderRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        await this._context.Orders.AddAsync(order);

        await this._context.SaveChangesAsync();

        return order;
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await this.GetByIdAsync(id);

        if (order is null)
            return;

        order.Status = OrderStatus.Cancelled;

        await this._context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
    {
        var ordersQuery = this._context.Orders
            .Include(o => o.Table)
            .Include(o => o.Waiter)
            .Include(o => o.Items)
            .AsQueryable();

        var activeOrders = await ordersQuery
            .Where(o => o.Status != OrderStatus.Completed && o.Status != OrderStatus.Cancelled)
            .ToListAsync();

        return activeOrders;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var ordersQuery = this._context.Orders
            .Include(o => o.Table)
            .Include(o => o.Waiter)
            .Include(o => o.Items)
            .AsQueryable();

        var orders = await ordersQuery.ToListAsync();

        return orders;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var ordersQuery = this._context.Orders
            .Include(o => o.Table)
            .Include(o => o.Waiter)
            .Include(o => o.Items)
            .AsQueryable();

        var order = await ordersQuery.FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
            return null;

        return order;
    }

    public async Task<IEnumerable<Order>> GetByTableIdAsync(Guid tableId)
    {
        var ordersQuery = this._context.Orders
            .Include(o => o.Table)
            .Include(o => o.Waiter)
            .Include(o => o.Items)
            .AsQueryable();

        var orders = await ordersQuery
            .Where(o => o.TableId == tableId)
            .ToListAsync();

        return orders;
    }

    public async Task<IEnumerable<Order>> GetByWaiterIdAsync(Guid waiterId)
    {
        var ordersQuery = this._context.Orders
            .Include(o => o.Table)
            .Include(o => o.Waiter)
            .Include(o => o.Items)
            .AsQueryable();

        var orders = await ordersQuery
            .Where(o => o.WaiterId == waiterId)
            .ToListAsync();

        return orders;
    }

    public async Task UpdateAsync(Order order)
    {
        this._context.Orders.Update(order);

        await this._context.SaveChangesAsync();
    }
}
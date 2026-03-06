using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
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

    public Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderItem>> GetPendingItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }
}
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
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

    public Task<Order> CreateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetActiveOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetByTableIdAsync(Guid tableId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
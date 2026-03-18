using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetActiveOrdersAsync();
    Task<IEnumerable<Order>> GetByTableIdAsync(Guid tableId);
    Task<IEnumerable<Order>> GetByWaiterIdAsync(Guid waiterId);
    Task<Order> CreateAsync(Order order);
    Task UpdateAsync(Order order);
    Task<bool> CancelAsync(Guid id);
}

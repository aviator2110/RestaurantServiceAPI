using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IOrderItemRepository
{
    Task<OrderItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId);
    Task<IEnumerable<OrderItem>> GetPendingItemsAsync();
    Task<OrderItem> CreateAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(Guid id);
}

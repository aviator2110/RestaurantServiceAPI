using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetAvailableAsync();
    Task<IEnumerable<Product>> GetByCategoryAsync(MenuCategory category);
    Task<bool> ExistsByNameAsync(string name);
    Task<Product> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task<bool> SaveChangesAsync();
}

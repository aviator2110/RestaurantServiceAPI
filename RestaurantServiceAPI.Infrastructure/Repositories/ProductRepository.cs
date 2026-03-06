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

public class ProductRepository : IProductRepository
{
    private readonly RestaurantServiceDbContext _context;

    public ProductRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    public Task<Product> CreateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAvailableAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByCategoryAsync(MenuCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}

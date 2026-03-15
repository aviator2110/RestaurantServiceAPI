using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using RestaurantServiceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public async Task<Product> CreateAsync(Product product)
    {
        this._context.Products.Add(product);

        await this._context.SaveChangesAsync();

        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);

        if (product is null || !product.IsAvailable)
            throw new Exception("No product with this Id!");

        product.IsAvailable = false;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await this._context.Products
            .AnyAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await this._context.Products.ToListAsync();

        return products;
    }

    public async Task<IEnumerable<Product>> GetAvailableAsync()
    {
        var productsQuery = this._context.Products.AsQueryable();

        var products = await productsQuery.Where(p => p.IsAvailable == true).ToListAsync();

        return products;
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(MenuCategory category)
    {
        var productsQuery = this._context.Products.AsQueryable();

        var products = await productsQuery.Where(p => p.Category == category).ToListAsync();

        return products;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await this._context.Products.FindAsync(id);

        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        this._context.Products.Update(product);

        await this._context.SaveChangesAsync();
    }
}
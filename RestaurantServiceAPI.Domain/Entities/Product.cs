using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public MenuCategory Category { get; set; }
    public bool IsAvailable { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    private Product() { }

    public Product(string name, string description, decimal price, MenuCategory category, bool isAvailable)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        IsAvailable = isAvailable;
    }

    public void UpdateDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void ChangePrice(decimal price)
    {
        if (price <= 0)
            throw new InvalidOperationException("Price must be greater than zero");

        Price = price;
    }

    public void ChangeCategory(MenuCategory category)
    {
        Category = category;
    }

    public void Activate()
    {
        IsAvailable = true;
    }

    public void Deactivate()
    {
        IsAvailable = false;
    }
}
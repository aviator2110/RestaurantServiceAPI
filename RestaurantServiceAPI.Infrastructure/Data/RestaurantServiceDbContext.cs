using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Data;

public class RestaurantServiceDbContext : DbContext
{
    public RestaurantServiceDbContext(DbContextOptions options)
        : base(options)
    {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(order =>
        {
            order.HasKey(o => o.Id);
            order.HasOne(o => o.Table)
                  .WithMany()
                  .HasForeignKey(o => o.TableId)
                  .OnDelete(DeleteBehavior.Restrict);
            order.HasOne(o => o.Waiter)
                  .WithMany()
                  .HasForeignKey(o => o.WaiterId)
                  .OnDelete(DeleteBehavior.Restrict);
            order.HasMany(typeof(OrderItem), "_items")
                  .WithOne()
                  .HasForeignKey("OrderId")
                  .OnDelete(DeleteBehavior.Cascade);
            order.Ignore(o => o.TotalAmount);
        });

        modelBuilder.Entity<OrderItem>(orderItem =>
        {
            orderItem.HasKey(x => x.Id);
            orderItem.HasOne(x => x.Order)
                  .WithMany("_items")
                  .HasForeignKey(x => x.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
            orderItem.HasOne(x => x.Product)
                  .WithMany()
                  .HasForeignKey(x => x.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
            orderItem.Property(x => x.UnitPrice)
                  .HasPrecision(18, 2);
            orderItem.Property(x => x.Status)
                  .HasConversion<string>();
            orderItem.Ignore(x => x.TotalPrice);
        });

        modelBuilder.Entity<Product>(product =>
        {
            product.HasKey(p => p.Id);
            product.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(150);
            product.Property(p => p.Description)
                  .HasMaxLength(500);
            product.Property(p => p.Price)
                  .HasPrecision(18, 2);
            product.Property(p => p.Category)
                  .HasConversion<string>();
        });

        modelBuilder.Entity<Table>(table =>
        {
            table.HasKey(t => t.Id);
            table.Property(t => t.Number)
                  .IsRequired();
            table.HasIndex(t => t.Number)
                  .IsUnique();
        });

        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Id);
            user.Property(u => u.FirstName)
                  .IsRequired()
                  .HasMaxLength(100);
            user.Property(u => u.LastName)
                  .IsRequired()
                  .HasMaxLength(100);
            user.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(200);
            user.HasIndex(u => u.Email)
                  .IsUnique();
            user.Property(u => u.PasswordHash)
                  .IsRequired();
            user.Property(u => u.Role)
                  .HasConversion<string>();
            user.Property(u => u.WaiterPinHash)
                  .HasMaxLength(200)
                  .IsRequired(false);
        });
    }
}
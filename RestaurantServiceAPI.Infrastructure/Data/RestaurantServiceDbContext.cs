using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Data;

public class RestaurantServiceDbContext : DbContext
{
    public RestaurantServiceDbContext(DbContextOptions<RestaurantServiceDbContext> options)
        : base(options)
    {

    }

    public DbSet<Waiter> Waiters => Set<Waiter>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<PanelAccount> PanelAccounts => Set<PanelAccount>();

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
            order.HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            order.Ignore(o => o.TotalAmount);
            order.Property(o => o.Status)
                .HasConversion<string>();
            order.Property(o => o.StartedAt)
                .IsRequired();
            order.Property(o => o.CompletedAt)
                .IsRequired(false);
            order.Navigation(o => o.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<OrderItem>(orderItem =>
        {
            orderItem.HasKey(x => x.Id);
            orderItem.HasOne(x => x.Order)
                .WithMany(o => o.Items)
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
            product.Property(p => p.IsAvailable)
                .HasDefaultValue(true);
            product.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Table>(table =>
        {
            table.HasKey(t => t.Id);
            table.Property(t => t.Number)
                .IsRequired();
            table.HasIndex(t => t.Number)
                .IsUnique();
            table.Property(t => t.IsActive)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<Waiter>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(w => w.LastName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(w => w.PinHash)
                .IsRequired();
            entity.Property(w => w.IsActive)
                .HasDefaultValue(true);
            entity.Property(w => w.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<PanelAccount>(entity =>
        {
            entity.HasKey(pa => pa.Id);
            entity.Property(pa => pa.Login)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasIndex(pa => pa.Login)
                .IsUnique();
            entity.Property(pa => pa.PasswordHash)
                .IsRequired();
            entity.Property(pa => pa.PanelType)
                .HasConversion<string>()
                .IsRequired();
            entity.Property(pa => pa.IsActive)
                .HasDefaultValue(true);
            entity.Property(pa => pa.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<PanelAccount>().HasData(
            new PanelAccount
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Login = "admin",
                PasswordHash = "gwbklxLIu1mjYNfVsjYjpyAg3klYWmgQjqY2dR2ma1aKsBxNAowel4qrpt+DpE8+",
                PanelType = PanelType.Admin,
                IsActive = true,
                CreatedAt = new DateTimeOffset(
                    new DateTime(2026, 1, 1),
                    TimeSpan.Zero)
            },

            new PanelAccount
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Login = "waiter",
                PasswordHash = "J2+nogLVA4WpidMG1gpDFO3MeCa/B2z0zwz77y8QW7fX26nUCJmbQzzgeFAJ+F0Q",
                PanelType = PanelType.Waiter,
                IsActive = true,
                CreatedAt = new DateTimeOffset(
                    new DateTime(2026, 1, 1),
                    TimeSpan.Zero)
            },

            new PanelAccount
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Login = "cook",
                PasswordHash = "58d3nczSLDy1tNnuUKEZKzBp1PMUuXrUv9WoHGtWE4f3mnaHmSmxBWlApqGfKC8k",
                PanelType = PanelType.Cook,
                IsActive = true,
                CreatedAt = new DateTimeOffset(
                    new DateTime(2026, 1, 1),
                    TimeSpan.Zero)
            },

            new PanelAccount
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Login = "bartender",
                PasswordHash = "c+MmA7AFqIXx2PLmAYIbyYuq5TkRjKh3z3FNXQEcM/QfpXYm6lGuOF4UHHKdJTjO",
                PanelType = PanelType.Bartender,
                IsActive = true,
                CreatedAt = new DateTimeOffset(
                    new DateTime(2026, 1, 1),
                    TimeSpan.Zero)
            }
        );
    }
}
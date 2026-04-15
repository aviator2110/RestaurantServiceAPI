using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Infrastructure.Data;

namespace RestaurantServiceAPI.Infrastructure.Repositories;

public class WaiterRepository : IWaiterRepository
{
    private readonly RestaurantServiceDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public WaiterRepository(RestaurantServiceDbContext context, IPasswordHasher passwordHasher)
    {
        this._context = context;
        this._passwordHasher = passwordHasher;
    }

    public async Task<Waiter> CreateAsync(Waiter waiter)
    {
        await this._context.Waiters.AddAsync(waiter);

        await this._context.SaveChangesAsync();

        return waiter;
    }

    public async Task<bool> DeactivateAsync(Guid id)
    {
        var waiter = await this._context.Waiters.FindAsync(id);

        if (waiter is null)
            return false;

        if (!waiter.IsActive)
            return false;

        waiter.Deactivate();

        await this._context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Waiter>> GetActiveAsync()
    {
        var waitersQuery = this._context.Waiters.AsQueryable();

        var activeWaiters = await waitersQuery.Where(w => w.IsActive).ToListAsync();

        return activeWaiters;
    }

    public async Task<IEnumerable<Waiter>> GetAllAsync()
    {
        var waiters = await this._context.Waiters.ToListAsync();

        return waiters;
    }

    public async Task<Waiter?> GetByIdAsync(Guid id)
    {
        var waiter = await this._context.Waiters.FindAsync(id);

        if (waiter is null)
            return null;

        return waiter;
    }

    public async Task UpdateAsync(Waiter waiter)
    {
        this._context.Waiters.Update(waiter);

        await this._context.SaveChangesAsync();
    }

    public async Task<Waiter?> GetByPinAsync(string pin)
    {
        var waiters = await this._context.Waiters.ToListAsync();

        var isEqual = waiters.FirstOrDefault(w => this._passwordHasher.Verify(pin, w.PinHash));

        return isEqual;
    }
}
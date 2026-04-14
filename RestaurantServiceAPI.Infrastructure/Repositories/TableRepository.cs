using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Infrastructure.Data;

namespace RestaurantServiceAPI.Infrastructure.Repositories;

public class TableRepository : ITableRepository
{
    private readonly RestaurantServiceDbContext _context;

    public TableRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    public async Task<Table> CreateAsync(Table table)
    {
        await this._context.Tables.AddAsync(table);

        await this._context.SaveChangesAsync();

        return table;
    }

    public async Task<bool> DeactivateAsync(Guid id)
    {
        var table = await this.GetByIdAsync(id);

        if (table is null || table.IsActive is false)
            return false;

        table.IsActive = false;

        await this._context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Table>> GetActiveTablesAsync()
    {
        var tablesQuery = this._context.Tables.AsQueryable();

        var activeTables = await tablesQuery.Where(t => t.IsActive == true).ToListAsync();

        return activeTables;
    }

    public async Task<IEnumerable<Table>> GetAllAsync()
    {
        var tables = await this._context.Tables.ToListAsync();

        return tables;
    }

    public async Task<Table?> GetByIdAsync(Guid id)
    {
        var table = await this._context.Tables.FindAsync(id);

        if (table is null)
            return null;

        return table;
    }

    public async Task<Table?> GetByNumberAsync(int number)
    {
        var tablesQuery = this._context.Tables.AsQueryable();

        var table = await tablesQuery.FirstOrDefaultAsync(t => t.Number == number);

        if (table is null)
            return null;

        return table;
    }

    public async Task<bool> TableWithNumberExists(Table table, int number)
    {
        var tablesQuery = this._context.Tables.AsQueryable();

        var isExist = await tablesQuery.AnyAsync(t => t.Number == number && t.Id != table.Id);

        return isExist;
    }

    public async Task UpdateAsync(Table table)
    {
        this._context.Tables.Update(table);

        await this._context.SaveChangesAsync();
    }
}
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Repositories;

public class TableRepository : ITableRepository
{
    private readonly RestaurantServiceDbContext _context;

    public TableRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    public Task<Table> CreateAsync(Table table)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Table>> GetActiveTablesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Table>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Table?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Table?> GetByNumberAsync(int number)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Table table)
    {
        throw new NotImplementedException();
    }
}
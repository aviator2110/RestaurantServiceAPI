using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface ITableRepository
{
    Task<Table?> GetByIdAsync(Guid id);
    Task<Table?> GetByNumberAsync(int number);
    Task<IEnumerable<Table>> GetAllAsync();
    Task<IEnumerable<Table>> GetActiveTablesAsync();
    Task<Table> CreateAsync(Table table);
    Task UpdateAsync(Table table);
    Task<bool> DeactivateAsync(Guid id);
    Task<bool> TableWithNumberExists(int number);
}

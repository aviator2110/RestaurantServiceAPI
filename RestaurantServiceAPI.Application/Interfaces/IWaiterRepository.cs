using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IWaiterRepository
{
    Task<Waiter?> GetByIdAsync(Guid id);
    Task<IEnumerable<Waiter>> GetAllAsync();
    Task<IEnumerable<Waiter>> GetActiveAsync();
    Task<Waiter> CreateAsync(Waiter waiter);
    Task UpdateAsync(Waiter waiter);
    Task<bool> DeactivateAsync(Guid id);
    Task<Waiter?> GetByPinAsync(string pin);
}
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(string email, string password);
    Task<UserRole> GetUserRoleByUserIdAsync(Guid id);
    Task<User> CreateUserAsync(User user);
    Task ChangeUserRoleByEmailAsync(string email, string role);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
    Task<bool> UserWithEmailExistsAsync(string email);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}
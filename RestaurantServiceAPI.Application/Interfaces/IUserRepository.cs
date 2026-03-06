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
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(string email, string password);
    Task<UserRole> GetUserRoleByUserIdAsync(string id);
    Task<string> CreateUserAsync(CreateUserRequestDto request);
    Task ChangeUserRoleByEmailAsync(string email, string role);
}

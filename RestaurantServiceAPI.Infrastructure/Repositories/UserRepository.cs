using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using RestaurantServiceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RestaurantServiceDbContext _context;

    public UserRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    public Task ChangeUserRoleByEmailAsync(string email, string role)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckPasswordAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateUserAsync(CreateUserRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<UserRole> GetUserRoleByUserIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}
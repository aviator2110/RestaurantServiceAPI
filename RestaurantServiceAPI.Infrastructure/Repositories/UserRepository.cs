using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using RestaurantServiceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

    public async Task<User> CreateUserAsync(User user)
    {
        user.PasswordHash = this.Hash(user.PasswordHash);

        await this._context.Users.AddAsync(user);

        await this._context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var usersQuery = this._context.Users.AsQueryable();

        var user = await usersQuery
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
            return null;

        return user;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        var user = await this._context.Users.FindAsync(id);

        if (user is null)
            return null;

        return user;
    }

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await this.GetUserByEmailAsync(email);

        if (user is null)
            return false;

        var passwordHash = this.Hash(password);

        return user.PasswordHash == passwordHash;
    }

    public async Task<UserRole> GetUserRoleByUserIdAsync(Guid id)
    {
        var user = await this.GetUserByIdAsync(id);

        if (user is null)
            throw new Exception("User not found");

        return user.Role;
    }

    public async Task ChangeUserRoleByEmailAsync(string email, string role)
    {
        var user = await this.GetUserByEmailAsync(email);

        if (user is null)
            return;

        if (!Enum.TryParse<UserRole>(role, true, out var parsedRole))
            return;

        user.Role = parsedRole;

        await this._context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await this._context.Users.ToListAsync();

        return users;
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        var usersQuery = this._context.Users.AsQueryable();

        var users = await usersQuery
            .Where(u => u.IsActive == true)
            .ToListAsync();

        return users;
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
    {
        var usersQuery = this._context.Users.AsQueryable();

        var users = await usersQuery
            .Where(u => u.Role == role)
            .ToListAsync();

        return users;
    }

    public async Task<bool> UserWithEmailExistsAsync(string email)
    {
        var isExists = await this._context.Users
            .AnyAsync(u => u.Email == email);

        return isExists;
    }

    public async Task UpdateAsync(User user)
    {
        this._context.Users.Update(user);

        await this._context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await this.GetUserByIdAsync(id);

        if (user is null || user.IsActive is false)
            return;

        user.Deactivate();

        await this._context.SaveChangesAsync();
    }

    private string Hash(string input)
    {
        using var sha = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(input);

        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}
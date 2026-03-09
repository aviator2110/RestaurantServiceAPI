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

    public async Task<string> CreateUserAsync(CreateUserRequestDto request)
    {
        var passwordHash = this.Hash(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = request.Role,
            IsActive = true
        };

        await this._context.Users.AddAsync(user);

        await this._context.SaveChangesAsync();

        return user.Id.ToString();
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

    public async Task<User?> GetUserByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out var userId))
            return null;

        var user = await this._context.Users.FindAsync(userId);

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

    public async Task<UserRole> GetUserRoleByUserIdAsync(string id)
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

    private string Hash(string input)
    {
        using var sha = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(input);

        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}
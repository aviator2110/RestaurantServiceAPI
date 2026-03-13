using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? WaiterPinHash { get; set; } = null;
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    private User() { }

    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        UserRole role)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidOperationException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidOperationException("Last name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidOperationException("Email is required.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidOperationException("Password hash is required.");

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    public void UpdateProfile(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidOperationException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidOperationException("Last name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidOperationException("Email is required.");

        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void ChangeRole(UserRole role)
    {
        Role = role;

        if (role != UserRole.Waiter)
            WaiterPinHash = null;
    }

    public void ChangePassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidOperationException("Password hash is required.");

        PasswordHash = passwordHash;
    }

    public void AssignWaiterPin(string pinHash)
    {
        if (Role != UserRole.Waiter)
            throw new InvalidOperationException("Only waiter can have a PIN.");

        if (string.IsNullOrWhiteSpace(pinHash))
            throw new InvalidOperationException("PIN hash is required.");

        WaiterPinHash = pinHash;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
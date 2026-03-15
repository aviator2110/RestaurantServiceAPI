using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Entities;

public class Waiter
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PinHash { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    private Waiter() { }

    public Waiter(string firstName, string lastName, string pinHash)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        PinHash = pinHash;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void ChangePin(string pinHash)
    {
        PinHash = pinHash;
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
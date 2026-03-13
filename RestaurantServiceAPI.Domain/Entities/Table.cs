using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Entities;

public class Table
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public bool IsActive { get; set; }

    private Table() { }

    public Table(int number, bool isActive)
    {
        if (number <= 0)
            throw new InvalidOperationException("Table number must be greater than zero.");

        Id = Guid.NewGuid();
        Number = number;
        IsActive = isActive;
    }

    public void UpdateNumber(int number)
    {
        if (number <= 0)
            throw new InvalidOperationException("Table number must be greater than zero.");

        Number = number;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Update(int number, bool isActive)
    {
        UpdateNumber(number);

        if (isActive)
            Activate();
        else
            Deactivate();
    }
}
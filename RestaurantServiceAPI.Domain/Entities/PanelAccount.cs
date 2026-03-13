using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Entities;

public class PanelAccount
{
    public Guid Id { get; private set; }
    public string Login { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public PanelType PanelType { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private PanelAccount() { }

    public PanelAccount(string login, string passwordHash, PanelType panelType)
    {
        Id = Guid.NewGuid();
        Login = login;
        PasswordHash = passwordHash;
        PanelType = panelType;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
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
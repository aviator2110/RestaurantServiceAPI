using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Services;

public class PanelAuthService : IPanelAuthService
{
    private readonly RestaurantServiceDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public PanelAuthService(RestaurantServiceDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> SignInAsync(HttpContext httpContext, string login, string password)
    {
        var account = await _context.PanelAccounts
            .FirstOrDefaultAsync(x => x.Login == login && x.IsActive);

        if (account is null)
            return false;

        var isPasswordValid = _passwordHasher.Verify(password, account.PasswordHash);

        if (!isPasswordValid)
            return false;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Name, account.Login),
            new Claim(ClaimTypes.Role, account.PanelType.ToString())
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return true;
    }

    public async Task SignOutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IPanelAuthService
{
    Task<bool> SignInAsync(HttpContext httpContext, string login, string password);
    Task SignOutAsync(HttpContext httpContext);
}
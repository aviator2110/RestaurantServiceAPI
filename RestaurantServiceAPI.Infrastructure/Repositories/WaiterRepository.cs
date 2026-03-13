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

public class WaiterRepository : IWaiterRepository
{
    private readonly RestaurantServiceDbContext _context;

    public WaiterRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    
}
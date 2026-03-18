using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Repositories;

public class PanelAccountRepository : IPanelAccountRepository
{
    private readonly RestaurantServiceDbContext _context;

    public PanelAccountRepository(RestaurantServiceDbContext context)
    {
        this._context = context;
    }

    public async Task<IEnumerable<PanelAccount>> GetAllAsync()
    {
        var panelAccounts = await this._context.PanelAccounts.ToListAsync();

        return panelAccounts;
    }

    public async Task<PanelAccount?> GetByIdAsync(Guid id)
    {
        var panelAccount = await this._context.PanelAccounts.FindAsync(id);

        return panelAccount;
    }

    public async Task<PanelAccount?> GetByLoginAsync(string login)
    {
        var panelAccountsQuery = this._context.PanelAccounts.AsQueryable();

        var panelAccount = await panelAccountsQuery.FirstOrDefaultAsync(pa => pa.Login == login);

        return panelAccount;
    }

    public async Task UpdateAsync(PanelAccount account)
    {
        this._context.PanelAccounts.Update(account);

        await this._context.SaveChangesAsync();
    }
}
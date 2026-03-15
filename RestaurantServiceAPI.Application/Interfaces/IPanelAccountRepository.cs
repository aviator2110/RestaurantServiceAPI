using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IPanelAccountRepository
{
    Task<PanelAccount?> GetByIdAsync(Guid id);
    Task<PanelAccount?> GetByLoginAsync(string login);
    Task<IEnumerable<PanelAccount>> GetAllAsync();
    Task UpdateAsync(PanelAccount account);
}
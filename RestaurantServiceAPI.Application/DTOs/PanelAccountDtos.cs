using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.DTOs;

public class PanelAccountResponseDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public PanelType PanelType { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
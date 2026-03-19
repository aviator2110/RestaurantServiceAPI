using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Interfaces;

public interface IJwtTokenService
{
    LoginResponseDto CreateToken(PanelAccount account);
}

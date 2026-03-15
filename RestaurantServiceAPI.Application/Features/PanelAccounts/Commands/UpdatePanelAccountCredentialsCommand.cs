using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.PanelAccounts.Commands;

public record UpdatePanelAccountCredentialsCommand(Guid Id, string prevPassword, string newPassword)
    : IRequest<PanelAccountResponseDto>;
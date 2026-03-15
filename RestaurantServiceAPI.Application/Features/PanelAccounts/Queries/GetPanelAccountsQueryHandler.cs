using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.PanelAccounts.Queries;

public class GetPanelAccountsQueryHandler : IRequestHandler<GetPanelAccountsQuery, IEnumerable<PanelAccountResponseDto>>
{
    private readonly IPanelAccountRepository _panelAccountRepository;
    private readonly IMapper _mapper;

    public GetPanelAccountsQueryHandler(IPanelAccountRepository panelAccountRepository, IMapper mapper)
    {
        this._panelAccountRepository = panelAccountRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<PanelAccountResponseDto>> Handle(GetPanelAccountsQuery request, CancellationToken cancellationToken)
    {
        var panelAccounts = await this._panelAccountRepository.GetAllAsync();

        return this._mapper.Map<IEnumerable<PanelAccountResponseDto>>(panelAccounts);
    }
}
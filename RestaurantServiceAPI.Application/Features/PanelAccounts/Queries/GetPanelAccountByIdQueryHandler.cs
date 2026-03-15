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

public class GetPanelAccountByIdQueryHandler : IRequestHandler<GetPanelAccountByIdQuery, PanelAccountResponseDto>
{
    private readonly IPanelAccountRepository _panelAccountRepository;
    private readonly IMapper _mapper;

    public GetPanelAccountByIdQueryHandler(IPanelAccountRepository panelAccountRepository, IMapper mapper)
    {
        this._panelAccountRepository = panelAccountRepository;
        this._mapper = mapper;
    }

    public async Task<PanelAccountResponseDto> Handle(GetPanelAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var panelAccount = await this._panelAccountRepository.GetByIdAsync(request.Id);

        if (panelAccount is null)
            throw new Exception("Panel account with this Id doesn't exist!");

        return this._mapper.Map<PanelAccountResponseDto>(panelAccount);
    }
}
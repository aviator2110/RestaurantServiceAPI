using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Waiters.Queries;

public class GetActiveWaitersQueryHandler : IRequestHandler<GetActiveWaitersQuery, IEnumerable<WaiterResponseDto>>
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IMapper _mapper;

    public GetActiveWaitersQueryHandler(IWaiterRepository waiterRepository, IMapper mapper)
    {
        this._waiterRepository = waiterRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<WaiterResponseDto>> Handle(GetActiveWaitersQuery request, CancellationToken cancellationToken)
    {
        var waiters = await this._waiterRepository.GetActiveAsync();

        return this._mapper.Map<IEnumerable<WaiterResponseDto>>(waiters);
    }
}
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

public class GetWaiterByPinQueryHandler : IRequestHandler<GetWaiterByPinQuery, WaiterResponseDto>
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IMapper _mapper;

    public GetWaiterByPinQueryHandler(IWaiterRepository waiterRepository, IMapper mapper)
    {
        this._waiterRepository = waiterRepository;
        this._mapper = mapper;
    }

    public async Task<WaiterResponseDto> Handle(GetWaiterByPinQuery request, CancellationToken cancellationToken)
    {
        var waiter = await this._waiterRepository.GetByPinAsync(request.pin);

        if (waiter is null)
            throw new Exception("Waiter with this Pin doesn't exist!");

        return this._mapper.Map<WaiterResponseDto>(waiter);
    }
}

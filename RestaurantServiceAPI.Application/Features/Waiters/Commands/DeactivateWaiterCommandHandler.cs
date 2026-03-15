using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Waiters.Commands;

public class DeactivateWaiterCommandHandler : IRequestHandler<DeactivateWaiterCommand, WaiterResponseDto>
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IMapper _mapper;

    public DeactivateWaiterCommandHandler(IWaiterRepository waiterRepository, IMapper mapper)
    {
        this._waiterRepository = waiterRepository;
        this._mapper = mapper;
    }

    public async Task<WaiterResponseDto> Handle(DeactivateWaiterCommand request, CancellationToken cancellationToken)
    {
        var waiter = await this._waiterRepository.GetByIdAsync(request.Id);

        if (waiter is null)
            throw new Exception("Waiter with this Id doesn't exist!");

        waiter.Deactivate();

        await this._waiterRepository.UpdateAsync(waiter);

        return this._mapper.Map<WaiterResponseDto>(waiter);
    }
}
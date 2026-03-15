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

public class UpdateWaiterDetailsCommandHandler : IRequestHandler<UpdateWaiterDetailsCommand, WaiterResponseDto>
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IMapper _mapper;

    public UpdateWaiterDetailsCommandHandler(IWaiterRepository waiterRepository, IMapper mapper)
    {
        this._waiterRepository = waiterRepository;
        this._mapper = mapper;
    }

    public async Task<WaiterResponseDto> Handle(UpdateWaiterDetailsCommand request, CancellationToken cancellationToken)
    {
        var waiter = await this._waiterRepository.GetByIdAsync(request.Id);

        if (waiter is null)
            throw new Exception("Waiter with this Id doesn't exist!");

        waiter.UpdateProfile(request.updateRequest.FirstName, request.updateRequest.LastName);

        await this._waiterRepository.UpdateAsync(waiter);

        return this._mapper.Map<WaiterResponseDto>(waiter);
    }
}
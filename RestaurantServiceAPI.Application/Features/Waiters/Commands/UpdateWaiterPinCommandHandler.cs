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

public class UpdateWaiterPinCommandHandler : IRequestHandler<UpdateWaiterPinCommand, WaiterResponseDto>
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateWaiterPinCommandHandler(IWaiterRepository waiterRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        this._waiterRepository = waiterRepository;
        this._mapper = mapper;
        this._passwordHasher = passwordHasher;
    }

    public async Task<WaiterResponseDto> Handle(UpdateWaiterPinCommand request, CancellationToken cancellationToken)
    {
        var waiter = await this._waiterRepository.GetByIdAsync(request.Id);

        if (waiter is null)
            throw new Exception("Waiter with this Id doesn't exist!");

        var isEqual = this._passwordHasher.Verify(request.NewPin, waiter.PinHash);

        if (isEqual)
            throw new Exception("Previous pin is the same with new pin code!");

        var newPinHash = this._passwordHasher.Hash(request.NewPin);

        waiter.ChangePin(newPinHash);

        await this._waiterRepository.UpdateAsync(waiter);

        return this._mapper.Map<WaiterResponseDto>(waiter);
    }
}
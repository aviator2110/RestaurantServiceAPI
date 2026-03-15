using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Waiters.Commands;

public class CreateWaiterCommandHandler : IRequestHandler<CreateWaiterCommand, WaiterResponseDto>
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CreateWaiterCommandHandler(IWaiterRepository waiterRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        this._waiterRepository = waiterRepository;
        this._mapper = mapper;
        this._passwordHasher = passwordHasher;
    }

    public async Task<WaiterResponseDto> Handle(CreateWaiterCommand request, CancellationToken cancellationToken)
    {
        var pinHash = this._passwordHasher.Hash(request.CreateRequest.Pin);

        var waiter = new Waiter(
            request.CreateRequest.FirstName,
            request.CreateRequest.LastName,
            pinHash);

        await this._waiterRepository.CreateAsync(waiter);

        return this._mapper.Map<WaiterResponseDto>(waiter);
    }
}
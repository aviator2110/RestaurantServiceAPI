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

namespace RestaurantServiceAPI.Application.Features.Tables.Commands;

public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, TableResponseDto>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public CreateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
    {
        this._tableRepository = tableRepository;
        this._mapper = mapper;
    }

    public async Task<TableResponseDto> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var table = new Table(request.CreateRequest.Number, request.CreateRequest.IsActive);
        
        bool isExists = await this._tableRepository.TableWithNumberExists(table, request.CreateRequest.Number);

        if (isExists)
            throw new Exception("Table with this number already exists!");

        await this._tableRepository.CreateAsync(table);

        return this._mapper.Map<TableResponseDto>(table);
    }
}
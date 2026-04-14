using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Tables.Commands;

public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, TableResponseDto>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public UpdateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
    {
        this._tableRepository = tableRepository;
        this._mapper = mapper;
    }

    public async Task<TableResponseDto> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await this._tableRepository.GetByIdAsync(request.Id);

        if (table is null)
            throw new Exception("Table with this Id doesn't exist!");

        var isExists = await this._tableRepository.TableWithNumberExists(table, request.UpdateRequest.Number);

        if (isExists)
            throw new Exception("Table with this number already exists!");

        table.Update(request.UpdateRequest.Number, request.UpdateRequest.IsActive);

        await this._tableRepository.UpdateAsync(table);

        return this._mapper.Map<TableResponseDto>(table);
    }
}
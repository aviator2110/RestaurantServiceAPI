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

public class DeactiveTableCommandHandler : IRequestHandler<DeactiveTableCommand, TableResponseDto>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public DeactiveTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
    {
        this._tableRepository = tableRepository;
        this._mapper = mapper;
    }

    public async Task<TableResponseDto> Handle(DeactiveTableCommand request, CancellationToken cancellationToken)
    {
        var table = await this._tableRepository.GetByIdAsync(request.Id);

        if (table is null)
            throw new Exception("Table with this Id doesn't exist!");

        if (!table.IsActive)
            throw new Exception("This table already deactive!");

        table.Deactivate();

        await this._tableRepository.UpdateAsync(table);

        return this._mapper.Map<TableResponseDto>(table);
    }
}
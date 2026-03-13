using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.Tables.Queries;

public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableResponseDto>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public GetTableByIdQueryHandler(ITableRepository tableRepository, IMapper mapper)
    {
        this._tableRepository = tableRepository;
        this._mapper = mapper;
    }

    public async Task<TableResponseDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        var table = await this._tableRepository.GetByIdAsync(request.Id);

        if (table is null)
            throw new Exception("Table with this Id doesn't exist");

        return this._mapper.Map<TableResponseDto>(table);
    }
}
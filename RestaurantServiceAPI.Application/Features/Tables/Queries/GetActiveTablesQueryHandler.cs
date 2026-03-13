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

public class GetActiveTablesQueryHandler : IRequestHandler<GetActiveTablesQuery, IEnumerable<TableResponseDto>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public GetActiveTablesQueryHandler(ITableRepository tableRepository, IMapper mapper)
    {
        this._tableRepository = tableRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<TableResponseDto>> Handle(GetActiveTablesQuery request, CancellationToken cancellationToken)
    {
        var activeTables = await this._tableRepository.GetActiveTablesAsync();

        return this._mapper.Map<IEnumerable<TableResponseDto>>(activeTables);
    }
}
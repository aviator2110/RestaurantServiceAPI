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

public class GetTablesQueryHandler : IRequestHandler<GetTablesQuery, IEnumerable<TableResponseDto>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public GetTablesQueryHandler(ITableRepository tableRepository, IMapper mapper)
    {
        this._tableRepository = tableRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<TableResponseDto>> Handle(GetTablesQuery request, CancellationToken cancellationToken)
    {
        var tables = await this._tableRepository.GetAllAsync();

        return this._mapper.Map<IEnumerable<TableResponseDto>>(tables);
    }
}
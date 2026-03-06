using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.DTOs;

public class TableResponseDto
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public bool IsActive { get; set; }
}

public class CreateTableRequestDto
{
    public int Number { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateTableRequestDto
{
    public int Number { get; set; }
    public bool IsActive { get; set; }
}
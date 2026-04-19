using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Enums;

public enum OrderStatus
{
    Created,                // Order created             
    InProgress,             // At least one element is ready
    Served,                 // Served to the client
    Cancelled,              // Cancelled
    Completed,              // Order closed
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Domain.Enums;

public enum OrderItemStatus
{
    Pending,            // awaiting preparation
    Preparing,          // cooking
    Ready,              // ready
    Served,             // served to customer
    Cancelled,          // canceled
}

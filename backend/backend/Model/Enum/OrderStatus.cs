using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model.Enum
{
    public enum OrderStatus
    {
        OrderRequest,   
        PickUpRequest,  //user request to pickup the order
        Pickup,     //order ready for pickup
        Packaging,
        AssignedForDelivery,
        InTransport,
        Delivered,
    }
}

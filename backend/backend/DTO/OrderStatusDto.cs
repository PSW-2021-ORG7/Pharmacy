using backend.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class OrderStatusDto
    {
        public int Order_id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderStatusDto(){}
    }
}

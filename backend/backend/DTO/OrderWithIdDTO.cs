using backend.Model;
using backend.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class OrderWithIdDTO
    {
        public int Order_Id { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public Boolean deliveryReqired { get; set; }

        public String UserId { get; set; }
    }
}

using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class AdDTO
    {
        public String Title { get; set; }

        public String Content { get; set; }

        public List<OrderItem> OrderItems;

        public DateTime CreationDate { get; set; }

        public DateTime PromotionEndDate { get; set; }
    }
}

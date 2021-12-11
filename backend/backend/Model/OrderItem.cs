using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class OrderItem
    {
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }

        public OrderItem()
        {
        }
    }
}

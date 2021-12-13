using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class MakeAnOrderDTO
    {
        public string shoppingCartId { get; set; }
        public string delivery { get; set; }

        public MakeAnOrderDTO() { }
    }
}

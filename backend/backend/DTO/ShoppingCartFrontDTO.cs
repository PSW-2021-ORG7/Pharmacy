using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class ShoppingCartFrontDTO
    {
        public String ShoppingCart_Id;
        public String User_Id;
        public List<ShoppingCartItemFrontDTO> items;
        public String finalPrice;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class UpdateShoppingCartsItemQuantityDTO
    {
        public int ShoppingCarts_Id;
        public int newQuantity;
        public int ShoppingCartsItem_Id;

        public UpdateShoppingCartsItemQuantityDTO() { }
    }
}

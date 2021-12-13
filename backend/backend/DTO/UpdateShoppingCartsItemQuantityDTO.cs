using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class UpdateShoppingCartsItemQuantityDTO
    {
        public int ShoppingCarts_Id { get; set; }
        public int newQuantity { get; set; }
        public int ShoppingCartsItem_Id { get; set; }

        public UpdateShoppingCartsItemQuantityDTO() { }
    }
}

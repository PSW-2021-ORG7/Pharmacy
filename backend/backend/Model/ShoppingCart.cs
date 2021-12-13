using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("ShoppingCarts")]
    public class ShoppingCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ShoppingCart_Id { get; set; } 

        [Required]
        public List<OrderItem> ShoppingCartItem { get; set; }

        [Required]
        public User User { get; set; }

        public ShoppingCart() { }
        public ShoppingCart(User u, List<OrderItem> items, int id)
        {
            this.ShoppingCart_Id = id;
            this.User = u;
            this.ShoppingCartItem = new List<OrderItem>();
        }

        public double getFinalPrice()
        {
            double finalPrice = 0;
            foreach(OrderItem o in ShoppingCartItem)
            {
                finalPrice += o.getPriceForAll();
            }
            return finalPrice;
        }
    }
}

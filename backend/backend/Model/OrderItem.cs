using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        public Medicine Medicine { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public double PriceForSingleEntity { get; set; }

        public OrderItem()
        {
        }

        public double getPriceForAll()
        {
            return Quantity * PriceForSingleEntity;
        }
    }
}

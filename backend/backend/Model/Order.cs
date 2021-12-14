﻿using backend.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Order_Id { get; set; }

        [Required]
        public List<OrderItem> OrderItems { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public Boolean deliveryReqired { get; set; }

        public Order(){}

        public Order(int id, OrderStatus status)
        {
            OrderDate = DateTime.Now;
            Status = status;
            Order_Id = id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLibrary.Enum;

namespace DataAccessLibrary
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public bool IsDelete { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public EnumOrderStatus Status { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}

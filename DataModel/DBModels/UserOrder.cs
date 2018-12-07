
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    [Table("UserAndBookOrderLog")]
    public class UserOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Orderid { get; set; }

        [Required]
        public string UserId { get; set; }
        
        [Required]
        public int bookId { get; set; }
        
        [Required]
        public int cartId { get; set; }

        public DateTime BuyTime { get; set; }

        public double RefundAmount { get; set; }

        public bool IsReturned { get; set; }

        public DateTime ReturnTime { get; set; }

        public double BookEarning { get; set; }
    }
}
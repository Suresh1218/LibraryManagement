
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel
{
    [Table("UserOrderLog")]
    public class UserLog
    {
        public UserLog()
        {
            cart = new UserCart();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Orderid { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual UserCart cart { get; set; }

        public DateTime buyTime { get; set; }
    }

}
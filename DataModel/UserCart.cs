using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    [Table("UserCart")]
    public class UserCart
    {
        public UserCart()
        {
            selectedBooks = new List<Books>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        
        [Required]
        public string userId { get; set; }

        [Required]
        public virtual ICollection<Books> selectedBooks { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public double TotalAmount { get; set; }
        
    }
}

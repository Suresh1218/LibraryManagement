
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel
{
    [Table("LibraryBooks")]
    public class Books
    {
        public Books()
        {
            cart = new List<UserCart>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Author { get; set; }

        [Required]
        public int NoOfStock { get; set; }

        public int NoOfSoldBooks { get; set; }

        public int NoOfBooksIsInUse { get; set; }
        
        public double BookPrice { get; set; }

        public byte[] Image { get; set; }

        public string Category { get; set; }
        
        [NotMapped]
        public bool isAddedToCart { get; set; }

        public ICollection<UserCart> cart { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }
        
    }
}
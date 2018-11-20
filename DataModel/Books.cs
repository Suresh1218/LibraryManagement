
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel
{
    [Table("LibraryBooks")]
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Author { get; set; }

        [Required]
        public int NoOfStock { get; set; }

        public int NoOfBooksIsInUse { get; set; }
        
        public double BookPrice { get; set; }

        public string ImageUrl { get; set; }
    }
}
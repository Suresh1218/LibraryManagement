using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("LibraryBooks")]
    public class Books
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public int NoOfStock { get; set; }

        public string ImageUrl { get; set; }
    }
}
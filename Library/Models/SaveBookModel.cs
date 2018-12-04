using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class SaveBookModel
    {
        
        public string Name { get; set; }

        public string Author { get; set; }
        
        public int NoOfStock { get; set; }

        public int NoOfBooksIsInUse { get; set; }

        public double BookPrice { get; set; }

        public HttpFileCollectionBase ImageUrl { get; set; }

        public string Category { get; set; }
    }
}
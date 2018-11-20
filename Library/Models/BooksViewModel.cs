using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BooksViewModel
    {
        public List<Books> books { get; set; }
        public string User { get; set; }
    }
}
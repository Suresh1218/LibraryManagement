using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BooksViewModel
    {
        public string domain { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Books> books { get; set; }
        public bool IsAdmin { get; set; }

        public BooksViewModel()
        {
            Users = new List<ApplicationUser>();
        }
    }
}
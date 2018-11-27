using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DataModel.BookCategory;

namespace Library.Models
{
    public class BooksViewModel
    {
        public string domain { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Books> books { get; set; }
        public Dictionary<BookCategories, string> bookCategories { get; set; }
        public Dictionary<string, int> AuthorAndBookList { get; set; }
        public Dictionary<string, int> CategoriesAndCount { get; set; }

        public UserCart cart { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }

        public BooksViewModel()
        {
            books = new List<Books>();
            Users = new List<ApplicationUser>();
            bookCategories = new Dictionary<BookCategories, string>();
            AuthorAndBookList = new Dictionary<string, int>();
            CategoriesAndCount = new Dictionary<string, int>();
        }
    }
}
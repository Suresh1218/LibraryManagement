using LibraryManagement.Models;
using LibraryServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IUserService userSevice;

        public BookController(IBookService _bookService, IUserService _userSevice)
        {
            bookService = _bookService;
            userSevice = _userSevice;
        }
        // GET: Book
        public ActionResult AllGetBooks()
        {
            BooksViewModel model = new BooksViewModel
            {
                books = bookService.getAll().ToList()
            }; 
            return View(model);
        }

    }
}
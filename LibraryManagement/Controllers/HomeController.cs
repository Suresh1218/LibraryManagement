using LibraryManagement.Models;
using LibraryServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public HomeController(IUserService _userService, IBookService _bookService)
        {
            userService = _userService;
            bookService = _bookService;
        }

        public ActionResult AllBooks()
        {
            BooksViewModel model = new BooksViewModel
            {
                books = bookService.getAll().ToList()
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
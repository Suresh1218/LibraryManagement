using Library.Models;
using LibraryServise;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public HomeController(IUserService _userService, IBookService _bookService)
        {
            userService = _userService;
            bookService = _bookService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult AllBooks()
        {
            
            if (isAdminUser() || IsUser())
            {
                return RedirectToAction("HomeAsync", "Library");
            }
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

        [NonAction]
        public bool isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                return UserManager.IsInRole(user.GetUserId(), "Admin");
            }
            return false;
        }
        [NonAction]
        public bool IsUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                return UserManager.IsInRole(user.GetUserId(), "User");
            }
            return false;
        }
    }
}
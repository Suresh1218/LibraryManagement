using Library.Models;
using LibraryServise;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public LibraryController(IUserService _userService, IBookService _bookService)
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

        // GET: Library
        public ActionResult HomeAsync()
        {
            BooksViewModel model = new BooksViewModel();

            model.books = bookService.getAll().ToList();
            model.IsAdmin = isAdminUser();
            return View(model);
        }
        
        
        [Authorize(Roles ="Admin")]
        public ActionResult UploadBook()
        {
            BooksViewModel model = new BooksViewModel();
            model.domain = @"http://localhost:51353";
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewUsers()
        {
            BooksViewModel model = new BooksViewModel();
            model.Users = UserManager.Users.ToList();
            return View(model);
        }

        public bool isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                return UserManager.IsInRole(user.GetUserId(),"Admin");
            }
            return false;
        }
    }
}
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
        public async System.Threading.Tasks.Task<ActionResult> HomeAsync()
        {
            BooksViewModel model = new BooksViewModel();

            model.books = bookService.getAll().ToList();
            var userId = User.Identity.GetUserId();
            //var user = await UserManager.GetRoles();
            //model.User = userService.getUserRolle(User.Identity.GetUserId());
            
            model.User = "Admin";
            return View(model);
        }
        
        
        [Authorize(Roles ="")]
        public ActionResult UploadBook()
        {
            return View();
        }
    }
}
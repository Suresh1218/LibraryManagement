using DataModel;
using LibraryServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Library.Models;
using System.Web.Http.Cors;

namespace Library.Controllers
{
    [EnableCors(origins: "http://localhost:51353", headers: "*", methods: "*")]
    public class LibraryApiController : ApiController
    {
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public LibraryApiController(IUserService _userService, IBookService _bookService)
        {
            userService = _userService;
            bookService = _bookService;
        }
        
        [HttpPost]
        public IHttpActionResult SaveBook(Books book)
        {
            if (isAdminUser())
            {
                if (bookService.SaveBook(book))
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Unauthorized();
        }

        [NonAction]
        public bool isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                return UserManager.IsInRole(user.GetUserId(), "Admin");
            }
            return false;
        }
    }
}

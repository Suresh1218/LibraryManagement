﻿using DataModel;
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
using System.IO;

namespace Library.Controllers
{
    [EnableCors(origins: "http://localhost:51353", headers: "*", methods: "*")]
    public class LibraryApiController : ApiController
    {
        private readonly IUserService userService;
        private readonly IBookService bookService;
        private readonly ICartService cartService;

        public LibraryApiController(IUserService _userService, IBookService _bookService, ICartService _cartService)
        {
            cartService = _cartService;
            userService = _userService;
            bookService = _bookService;
        }
        
        [HttpPost]
        public IHttpActionResult AddBookToCart([FromUri]int bookId)
        {
            string uid = User.Identity.GetUserId();
            if (cartService.CountOfBooksInCart(uid) < 5)
            {
                Books selectBook = bookService.getBookById(bookId);
                if (bookService.IsBookAvailableInStock(selectBook))
                {
                    if (cartService.SaveOrUpdate(uid, selectBook))
                        return Ok("Added Successfully");
                }
            }
            return Ok("Only 5 books are allowed to Add in cart");
        }

        [HttpPost]
        public IHttpActionResult RemoveBookfromCart([FromUri]int bookId)
        {
            string uid = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(uid))
            {
                cartService.removeBookFromCart(uid, bookId);

                //decrement use count of book
                bookService.DecrementUseCount(bookId);

                return Ok("Removed Succefully");
            }
            return BadRequest("Invalid Request");
        }

        [HttpPost]
        public IHttpActionResult UpdateBook(Books book)
        {
            if (bookService.UpdateBook(book))
                return Ok("Success");
            return BadRequest();
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
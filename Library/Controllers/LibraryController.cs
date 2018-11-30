﻿using DataModel;
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
using static DataModel.BookCategory;

namespace Library.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IUserService userService;
        private readonly IBookService bookService;
        private readonly ICartService cartService;
        private string domain = @"http://localhost:51353";

        public LibraryController(IUserService _userService, IBookService _bookService, ICartService _cartService)
        {
            userService = _userService;
            bookService = _bookService;
            cartService = _cartService;
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
            model.domain = domain;
            var AllBooks = bookService.getAll().ToList();
            model.IsAdmin = isAdminUser();
            if (IsUser())
            {
                model.IsUser = true;
                List<string> cartAddedBooks = cartService.getAddedBooks(User.Identity.GetUserId());

                foreach (var book in cartAddedBooks)
                {
                    var bookmodel = AllBooks.Where(b => b.Name.Equals(book))
                                    .Select(b => new Books()
                                    {
                                        Name = b.Name,
                                        Id = b.Id,
                                        NoOfBooksIsInUse = b.NoOfBooksIsInUse,
                                        Author = b.Author,
                                        NoOfStock = b.NoOfStock,
                                        BookPrice = b.BookPrice,
                                        isAddedToCart = true,
                                        ImageUrl = b.ImageUrl,
                                    }).FirstOrDefault();
                    model.books.Add(bookmodel);
                }
                foreach (var book in AllBooks)
                {
                    if (!model.books.Any(b => b.Id == book.Id))
                        model.books.Add(book);
                }
            }
            else
            {
                model.books = AllBooks;
            }
            return View(model);
        }
        
        [Authorize(Roles ="Admin")]
        public ActionResult UploadBook()
        {
            BooksViewModel model = new BooksViewModel();
            model.domain = domain;
            
            var categories =  from BookCategories s in Enum.GetValues(typeof(BookCategories))
                                        select new { ID = s, Name = Enumreations.GetEnumDescription(s) };
            foreach (var category in categories)
            {
                model.bookCategories.Add(category.ID, category.Name);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewUsers()
        {
            BooksViewModel model = new BooksViewModel();
            List<ApplicationUser> users = UserManager.Users.ToList();
            
            var adminUsers = (from user in users
                             from rol in user.Roles
                             where rol.RoleId.Equals("1")
                             select user).ToList();

            if (adminUsers != null)
            {
                adminUsers.ForEach(admin => users.Remove(admin));
            }

            model.Users = users;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ViewAuthores()
        {
            BooksViewModel model = new BooksViewModel();
            model.IsAdmin = isAdminUser();
            model.IsUser = IsUser();
            model.AuthorAndBookList = bookService.getAuthoreList();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ViewCategories()
        {
            BooksViewModel model = new BooksViewModel();
            model.IsAdmin = isAdminUser();
            model.IsUser = IsUser();
            model.CategoriesAndCount = bookService.getCategoryList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewUserLog(string userId)
        {
            BooksViewModel model = new BooksViewModel();
            model.domain = domain;
            model.cart = cartService.GetCart(userId);
            return View(model);
        }

        [Authorize(Roles = "User")]
        public ActionResult checkCart()
        {
            string id = User.Identity.GetUserId();
            BooksViewModel model = new BooksViewModel();
            model.domain = domain;
            model.cart = cartService.GetCart(id);
            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult UpdateBookDetails(int id)
        {
            Books book = new Books();
            book = bookService.getBookById(id);
            return View(book);
        }

        [AllowAnonymous]
        public ActionResult CheckAuthoreBooks(string name = "Cris Hammer")
        {
            BooksViewModel model = new BooksViewModel();
            var AllBooks = bookService.getBooksOfAuthor(name).ToList();
            model.IsAdmin = isAdminUser();
            if (IsUser() && AllBooks != null) 
            {
                model.IsUser = true;
                List<string> cartAddedBooks = cartService.getAddedBookOfAuthor(User.Identity.GetUserId(),name);
                if (cartAddedBooks.Count > 0)
                {
                    foreach (var book in cartAddedBooks)
                    {
                        var bookmodel = AllBooks.Where(b => b.Name.Equals(book))
                                        .Select(b => new Books()
                                        {
                                            Name = b.Name,
                                            Id = b.Id,
                                            NoOfBooksIsInUse = b.NoOfBooksIsInUse,
                                            Author = b.Author,
                                            NoOfStock = b.NoOfStock,
                                            BookPrice = b.BookPrice,
                                            isAddedToCart = true,
                                            ImageUrl = b.ImageUrl,
                                        }).FirstOrDefault();
                        model.books.Add(bookmodel);
                    }
                    foreach (var book in AllBooks)
                    {
                        if (!model.books.Any(b => b.Id == book.Id))
                            model.books.Add(book);
                    }
                    (model.books).OrderByDescending(b => b.Id);
                }
                else
                    model.books = AllBooks;
            }
            else
            {
                model.books = AllBooks;
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult CheckCategoryBooks(string category = "Fiction")
        {
            BooksViewModel model = new BooksViewModel();
            //BookCategory.BookCategories mycategory = (BookCategory.BookCategories)Enum.Parse(typeof(BookCategory.BookCategories), category, true);
            var AllBooks = bookService.getBooksOfCategory(category).ToList();
            model.IsAdmin = isAdminUser();
            if (IsUser() && AllBooks != null)
            {
                model.IsUser = true;
                List<string> cartAddedBooks = cartService.getAddedBookOfCategory(User.Identity.GetUserId(), category);
                if (cartAddedBooks.Count > 0)
                {
                    foreach (var book in cartAddedBooks)
                    {
                        var bookmodel = AllBooks.Where(b => b.Name.Equals(book))
                                        .Select(b => new Books()
                                        {
                                            Name = b.Name,
                                            Id = b.Id,
                                            NoOfBooksIsInUse = b.NoOfBooksIsInUse,
                                            Author = b.Author,
                                            NoOfStock = b.NoOfStock,
                                            BookPrice = b.BookPrice,
                                            isAddedToCart = true,
                                            ImageUrl = b.ImageUrl,
                                        }).FirstOrDefault();
                        model.books.Add(bookmodel);
                    }
                    foreach (var book in AllBooks)
                    {
                        if (!model.books.Any(b => b.Id == book.Id))
                            model.books.Add(book);
                    }
                }
                else
                    model.books = AllBooks;
            }
            else
            {
                model.books = AllBooks;
            }
            return View(model);
        }

        [NonAction]
        public bool isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                return UserManager.IsInRole(user.GetUserId(),"Admin");
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
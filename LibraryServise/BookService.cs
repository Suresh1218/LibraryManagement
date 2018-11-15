using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServise
{
    public class BookService : IBookService
    {
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public BookService(IUserService _userService, IBookService _bookService)
        {
            userService = _userService;
            bookService = _bookService;
        }
    }

    public interface IBookService
    {

    }
}

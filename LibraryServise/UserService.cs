using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServise
{
    public class UserService : IUserService
    {
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public UserService(IUserService _userService,IBookService _bookService)
        {
            userService = _userService;
            bookService = _bookService;
        }
    }

    public interface IUserService
    {

    }
}

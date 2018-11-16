using DataModel;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServise
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        public BookService(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        }
        public IEnumerable<Books> getAll()
        {
            return bookRepository.GetAll().Where(b => b.NoOfStock > 0);
        }
    }

    public interface IBookService
    {
        IEnumerable<Books> getAll();
    }
}

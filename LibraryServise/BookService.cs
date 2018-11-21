using DataModel;
using DataModel.Infrastructure;
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
        private IUnitOfWork unitOfWork;
        private readonly IBookRepository bookRepository;
        public BookService(IBookRepository _bookRepository,IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            bookRepository = _bookRepository;
        }
        public IEnumerable<Books> getAll()
        {
            return bookRepository.GetAll().Where(b => b.NoOfStock > 0);
        }
        public bool SaveBook(Books book)
        {
            if (bookRepository.Add(book)!=null)
            {
                SaveChanges();
                return true;
            }
            return false;
        }
        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }

    public interface IBookService
    {
        void SaveChanges();
        IEnumerable<Books> getAll();
        bool SaveBook(Books book);
    }
}

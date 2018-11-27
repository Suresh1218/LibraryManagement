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

        public Books getBookById(int id)
        {
            return bookRepository.Query(b=>b.Id == id).FirstOrDefault();
        }

        public bool IsBookAvailableInStock(Books book)
        {
            Books available = bookRepository.Query(b => b.Id == book.Id && b.NoOfBooksIsInUse < b.NoOfStock).FirstOrDefault();
            if (available != null)
                return true;
            else
                return false;
        }

        public void UpDateUseCountOfBook(int id)
        {
            Books available = bookRepository.Query(b => b.Id ==id ).FirstOrDefault();
            if (available != null)
            {
                available.NoOfBooksIsInUse = available.NoOfBooksIsInUse + 1;
                SaveChanges();
            }
        }

        public bool UpdateBook(Books book)
        {
            Books dbBook = getBookById(book.Id);
            if (dbBook != null)
            {
                dbBook.Name = string.IsNullOrEmpty(book.Name) ? dbBook.Name : book.Name;
                dbBook.Author = string.IsNullOrEmpty(book.Author) ? dbBook.Author : book.Author;
                dbBook.BookPrice = book.BookPrice == 0 ? dbBook.BookPrice : book.BookPrice;
                dbBook.NoOfStock = book.NoOfStock == 0 ? dbBook.NoOfStock : book.NoOfStock;
                SaveChanges();
                return true;
            }
            return false;
        }
        public Dictionary<string,int> getAuthoreList()
        {
            Dictionary<string,int> Authors = new Dictionary<string, int>();
            List<Books> books = bookRepository.GetAll().ToList();
            foreach (var bk in books)
            {
                string name = bk.Author;
                int count = books.Count(b => b.Author == name);
                Authors.Add(name,count);
            }
            
            return Authors;
        }
        public Dictionary<string, int> getCategoryList()
        {
            Dictionary<string, int> Categories = new Dictionary<string, int>();
            List<Books> books = bookRepository.GetAll().ToList();
            foreach (var bk in books)
            {
                string name = bk.Category;
                int count = books.Count(b => b.Category == name);
                Categories.Add(name, count);
            }

            return Categories;
        }

        public IEnumerable<Books> getBooksOfAuthor(string name)
        {
            return bookRepository.Query(b => b.Author.Equals(name)).ToList();
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
        Books getBookById(int id);
        bool IsBookAvailableInStock(Books book);
        void UpDateUseCountOfBook(int id);
        bool UpdateBook(Books book);
        Dictionary<string, int> getAuthoreList();
        Dictionary<string, int> getCategoryList();
        IEnumerable<Books> getBooksOfAuthor(string name);
    }
}

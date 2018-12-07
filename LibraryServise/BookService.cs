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

        public BookService(IBookRepository _bookRepository, IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            bookRepository = _bookRepository;
        }

        public IEnumerable<Books> getAll()
        {
            return (bookRepository.GetAll().Where(b => b.NoOfStock > 0)).OrderByDescending(b => b.Id);
        }

        public bool SaveBook(Books book)
        {
            if (bookRepository.Add(book) != null)
            {
                SaveChanges();
                return true;
            }
            return false;
        }

        public Books getBookById(int id)
        {
            return bookRepository.Query(b => b.Id == id).FirstOrDefault();
        }

        public bool IsBookAvailableInStock(Books book)
        {
            Books available = bookRepository.Query(b => b.Id == book.Id && b.NoOfBooksIsInUse < b.NoOfStock, true).FirstOrDefault();
            if (available != null)
                return true;
            else
                return false;
        }

        public void UpDateUseCountOfBook(int id)
        {
            Books available = bookRepository.Query(b => b.Id == id).FirstOrDefault();
            if (available != null)
            {
                available.NoOfBooksIsInUse = available.NoOfBooksIsInUse + 1;
                SaveChanges();
            }
        }

        public void DecrementUseCount(int id)
        {
            Books available = bookRepository.Query(b => b.Id == id).FirstOrDefault();
            if (available != null && available.NoOfBooksIsInUse > 0)
            {
                available.NoOfBooksIsInUse = available.NoOfBooksIsInUse - 1;
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

        public Dictionary<string, int> getAuthoreList()
        {
            Dictionary<string, int> Authors = new Dictionary<string, int>();
            List<Books> books = bookRepository.GetAll().ToList();
            foreach (var bk in books)
            {
                string name = bk.Author;
                int count = books.Count(b => b.Author == name);
                if (!Authors.ContainsKey(name))
                    Authors.Add(name, count);
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
                if (!Categories.ContainsKey(name))
                    Categories.Add(name, count);

            }

            return Categories;
        }

        public IEnumerable<Books> getBooksOfAuthor(string name)
        {
            return bookRepository.Query(b => b.Author.Equals(name)).ToList();
        }

        public IEnumerable<Books> getBooksOfCategory(string name)
        {
            return bookRepository.Query(b => b.Category.Equals(name)).ToList();
        }

        public bool IsPresentAlready(string bookName, string AuthoreName)
        {
            Books book = bookRepository.Query(b => b.Name.ToLower().Equals(bookName.ToLower()) && b.Author.ToLower().Equals(AuthoreName.ToLower()), true).FirstOrDefault();
            if (book != null)
                return true;
            else
                return false;
        }

        public void DecrementUseCountOfBook(int bookId)
        {
            if (bookId != 0)
            {
                Books book = bookRepository.GetById(bookId);
                book.NoOfBooksIsInUse -=  1;
                bookRepository.Update(book);
                SaveChanges();
            }
        }

        public void DecrementStockCount(int bookId)
        {
            if (bookId != 0)
            {
                Books book = bookRepository.GetById(bookId);
                book.NoOfStock -=  1;
                book.NoOfSoldBooks += 1;
                bookRepository.Update(book);
                SaveChanges();
            }
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
        IEnumerable<Books> getBooksOfCategory(string name);
        bool IsPresentAlready(string bookName,string AuthoreName);
        void DecrementUseCountOfBook(int bookId);
        void DecrementUseCount(int bookId);
        void DecrementStockCount(int bookId);
    }
}

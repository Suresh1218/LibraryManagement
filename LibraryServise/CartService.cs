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
    public class CartService : ICartService
    {
        private IUnitOfWork unitOfWork;
        private readonly IUserCartRepository cartRepository;
        private readonly IBookService bookService;
        public CartService(IUserCartRepository _cartRepository, IUnitOfWork _unitOfWork, IBookService _bookService)
        {
            bookService = _bookService;
            unitOfWork = _unitOfWork;
            cartRepository = _cartRepository;
        }

        public UserCart GetCart(string uid)
        {
            return cartRepository.Query(x => x.userId == uid).FirstOrDefault();
        }

        public bool SaveOrUpdate(string uid,Books book)
        {
            var savecart = cartRepository.Query(x => x.userId.Equals(uid)).FirstOrDefault();
            if (savecart == null)
            {
                savecart = new UserCart();
                savecart.userId = uid;
                savecart.CreatedDate = DateTime.Now;
                savecart.selectedBooks.Add(book);
                savecart.TotalAmount = book.BookPrice;

                cartRepository.Add(savecart);
            }
            else
            {
                savecart.selectedBooks.Add(book);
                savecart.TotalAmount = savecart.selectedBooks.Sum(x => x.BookPrice);
            }
            SaveChanges();
            bookService.UpDateUseCountOfBook(book.Id);
            return true;
        }

        public List<string> getAddedBooks(string uid)
        {
            var cart = cartRepository.Query(c=>c.userId.Equals(uid)).FirstOrDefault();
            var names = (from name in cart.selectedBooks
                         select name.Name).ToList();
            return names;
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }

    public interface ICartService
    {
        bool SaveOrUpdate(string uid,Books book);
        List<string> getAddedBooks(string uid);
        UserCart GetCart(string uid);
        void SaveChanges();
    }
}

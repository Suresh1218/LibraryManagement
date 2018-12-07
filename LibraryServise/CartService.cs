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
        private readonly IOrderService orderService;
        public CartService(IUserCartRepository _cartRepository, IUnitOfWork _unitOfWork, IBookService _bookService, IOrderService _orderService)
        {
            orderService = _orderService;
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

            UserOrder order = new UserOrder();
            order.UserId = uid;
            order.bookId = book.Id;
            order.cartId = savecart.CartId;
            order.BuyTime = DateTime.UtcNow;
            order.IsReturned = false;
            order.ReturnTime = DateTime.UtcNow;
            orderService.AddOrder(order);
            
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

        public List<string> getAddedBookOfAuthor(string uid, string AuthoreName)
        {
            var cart = cartRepository.Query(c => c.userId.Equals(uid)).FirstOrDefault();
            var names = (from name in cart.selectedBooks
                         where name.Author.Equals(AuthoreName)
                         select name.Name).ToList();
            return names;
        }

        public List<string> getAddedBookOfCategory(string uid, string category)
        {
            var cart = cartRepository.Query(c => c.userId.Equals(uid)).FirstOrDefault();
            var names = (from name in cart.selectedBooks
                         where name.Category.Equals(category)
                         select name.Name).ToList();
            return names;
        }

        public int CountOfBooksInCart(string uid)
        {
            return cartRepository.Query(r => r.userId.Equals(uid)).FirstOrDefault().selectedBooks.Count;
        }

        public void removeBookFromCart(string uid, int bookId)
        {
            if (!string.IsNullOrEmpty(uid) && bookId != 0)
            {
                UserCart cart = cartRepository.Query(c => c.userId.Equals(uid)).FirstOrDefault();
                if (cart != null)
                {
                    cart.selectedBooks.Remove(cart.selectedBooks.Where(b=>b.Id==bookId).FirstOrDefault());
                    cartRepository.Update(cart);
                    SaveChanges();
                    orderService.ReturnBook(uid, cart.CartId, bookId);
                }
            }
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
        List<string> getAddedBookOfAuthor(string uid, string AuthoreName);
        List<string> getAddedBookOfCategory(string uid,string category);
        UserCart GetCart(string uid);
        void SaveChanges();
        int CountOfBooksInCart(string uid);
        void removeBookFromCart(string uid,int bookId);
    }
}

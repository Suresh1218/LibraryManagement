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
    public class OrderService : IOrderService
    {
        private IUnitOfWork unitOfWork;
        private readonly IUserLogRepository userLogRepository;
        private readonly IBookService bookService;
        public OrderService(IUnitOfWork _unitOfWork,IUserLogRepository _userLogRepository , IBookService _bookService)
        {
            bookService = _bookService;
            unitOfWork = _unitOfWork;
            userLogRepository = _userLogRepository;
        }

        public void AddOrder(UserOrder order)
        {
            userLogRepository.Add(order);
            SaveChanges();
        }

        public void Update5MonthOlderOrder(string uid, int cartId, int bookId)
        {
            UserOrder order = userLogRepository.Query(o => o.UserId.Equals(uid) && o.bookId == bookId && o.cartId == cartId && o.IsReturned == false).FirstOrDefault();
            order.IsReturned = true;
            order.RefundAmount = 0;
            order.BookEarning = 0;
            SaveChanges();
        }

        public void ReturnBook(string uid, int cartId, int bookId)
        {
            int value = 0;
            UserOrder order = userLogRepository.Query(o => o.UserId.Equals(uid) && o.bookId == bookId && o.cartId == cartId && o.IsReturned == false).FirstOrDefault();
            if (order != null)
            {
                int refundper =  DateTime.UtcNow.Day - order.BuyTime.Day;
                Books bk = bookService.getBookById(bookId);
                
                order.ReturnTime = DateTime.UtcNow;
                
                if(refundper > 0 && refundper < 5)
                {
                    BookCategory.refundPolicy.TryGetValue(refundper, out value);
                    order.IsReturned = true;
                    order.RefundAmount = (bk.BookPrice / 100) * value > 0 ? (bk.BookPrice / 100) * value : 0;
                    order.BookEarning = bk.BookPrice - order.RefundAmount > 0 ? bk.BookPrice - order.RefundAmount : 0;
                }
                
                SaveChanges();
            }
        }

        public double getBookEarnings(int bookId)
        {
            return (userLogRepository.Query(o => o.bookId == bookId).ToList()).Sum(b => b.BookEarning);
        }

        public List<UserOrder> getOrdersOfUser(string uid)
        {
            return userLogRepository.Query(o => o.UserId.Equals(uid)).ToList();
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }
    public interface IOrderService
    {
        void ReturnBook(string uid, int cartId, int bookId);
        UserOrder Update5MonthOlderOrder(string uid,int cartId,int bookId);
        void AddOrder(UserOrder order);
        double getBookEarnings(int bookId);
        List<UserOrder> getOrdersOfUser(string uid);
        void SaveChanges();
    }
}
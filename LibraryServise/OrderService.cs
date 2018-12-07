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

        public void ReturnBook(string uid, int cartId, int bookId)
        {
            int value = 0;
            UserOrder order = userLogRepository.Query(o => o.UserId.Equals(uid) && o.bookId == bookId && o.cartId == cartId && o.IsReturned == false).FirstOrDefault();
            if (order != null)
            {
                int refundper =  DateTime.UtcNow.Minute - order.BuyTime.Minute;
                Books bk = bookService.getBookById(bookId);
                
                order.ReturnTime = DateTime.UtcNow;
                if (refundper > 5)
                {
                    order.RefundAmount =  0;
                    order.BookEarning =  0;
                    order.IsReturned = false;
                    bookService.DecrementStockCount(bk.Id);
                }
                else if(refundper > 0 && refundper < 5)
                {
                    BookCategory.refundPolicy.TryGetValue(refundper, out value);
                    order.IsReturned = true;
                    order.RefundAmount = (bk.BookPrice / 100) * value > 0 ? (bk.BookPrice / 100) * value : 0;
                    order.BookEarning = bk.BookPrice - order.RefundAmount > 0 ? bk.BookPrice - order.RefundAmount : 0;
                }
                
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }
    public interface IOrderService
    {
        void ReturnBook(string uid, int cartId, int bookId);
        void AddOrder(UserOrder order);
        void SaveChanges();
    }
}
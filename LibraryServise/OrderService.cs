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
        public OrderService(IUnitOfWork _unitOfWork,IUserLogRepository _userLogRepository)
        {
            unitOfWork = _unitOfWork;
            userLogRepository = _userLogRepository;
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }
    public interface IOrderService
    {
        void SaveChanges();
    }
}

using DataModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repository
{
    public class UserLogRepository : RepositoryBase<UserOrder>, IUserLogRepository
    {
        public UserLogRepository(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public LibraryDBModel GetDBModel()
        {
            return base.DataContext;
        }
    }

    public interface IUserLogRepository : IRepository<UserOrder>
    {
        LibraryDBModel GetDBModel();
    }
}

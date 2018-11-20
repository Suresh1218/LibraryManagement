using DataModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repository
{
    public class UserRepository : RepositoryBase<UserLog>, IUserRepository
    {
        public UserRepository(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public LibraryDBModel GetDBContext()
        {
            return base.DataContext;   
        }
    }

    public interface IUserRepository : IRepository<UserLog>
    {
        LibraryDBModel GetDBContext();
    }
}

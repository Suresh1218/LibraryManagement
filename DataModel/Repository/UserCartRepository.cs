using DataModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repository
{
    public class UserCartRepository : RepositoryBase<UserCart>, IUserCartRepository
    {
        public UserCartRepository(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public LibraryDBModel GetDBModel()
        {
            return base.DataContext;
        }
    }

    public interface IUserCartRepository : IRepository<UserCart>
    {
        LibraryDBModel GetDBModel();
    }
}

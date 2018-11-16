using DataModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repository
{
    public class BookRepository : RepositoryBase<Books> ,IBookRepository
    {
        public BookRepository(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public LibraryDBModel GetDBModel()
        {
            return base.DataContext;
        }
    }

    public interface IBookRepository : IRepository<Books>
    {
        LibraryDBModel GetDBModel();
    }
}

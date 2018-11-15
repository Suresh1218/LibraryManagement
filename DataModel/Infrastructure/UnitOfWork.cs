using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryDBModel dbContext;
        private readonly IDataBaseFactory dbFactory;

        protected LibraryDBModel DbContext
        {
            get
            {
                dbContext = dbContext ?? dbFactory.GetInstance();
                if (dbContext.Database.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbContext.Database.Connection.Open();
                }
                return dbContext;
            }
        }

        public UnitOfWork(IDataBaseFactory DbFactory)
        {
            dbFactory = DbFactory;
        }

        public void SaveChanges()
        {
            if (DbContext.Database.Connection.State == System.Data.ConnectionState.Closed)
            {
                DbContext.Database.Connection.Open();
            }
            DbContext.SaveChanges();
        }

        public virtual async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public LibraryDBModel GetDbContext() => this.DbContext;
    }

    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}

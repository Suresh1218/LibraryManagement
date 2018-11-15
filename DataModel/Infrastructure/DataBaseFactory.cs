using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Infrastructure
{
    public class DataBaseFactory : Disposable,IDataBaseFactory
    {
        public DataBaseFactory() { }

        public DataBaseFactory(LibraryDBModel dataContext)
        {
            if (this.dataContext == null)
            {
                this.dataContext = dataContext;
            }
        }

        private LibraryDBModel dataContext;
        public LibraryDBModel GetInstance() => dataContext ?? (dataContext = new LibraryDBModel());

        protected override void DisposeCore()
        {
            try
            {
                if (dataContext != null)
                {
                    if (dataContext.Database != null && dataContext.Database.Connection.State == ConnectionState.Open)
                    {
                        dataContext.Database.Connection.Close();
                    }
                    dataContext.Dispose();
                    dataContext = null;
                }
            }
            catch (Exception ex)
            {
                
            }

        }
    }

    public interface IDataBaseFactory : IDisposable
    {
        LibraryDBModel GetInstance();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class, new()
    {
        private LibraryDBModel dataContext;
        protected readonly IDbSet<T> dbSet;

        protected RepositoryBase(IDataBaseFactory databaseFactori)
        {
            DataBaseFactory = databaseFactori;
            dbSet = DataContext.Set<T>();
        }

        protected IDataBaseFactory DataBaseFactory
        {
            get;
            private set;
        }

        protected LibraryDBModel DataContext
        {
            get
            {
                if (dataContext == null)
                {
                    dataContext = DataBaseFactory.GetInstance();
                    if (dataContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dataContext.Database.Connection.Open();
                    }
                }
                return dataContext;
            }
        }

        public virtual T Add(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity) => dbSet.Remove(entity);

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(int id) => dbSet.Find(id);

        public virtual IEnumerable<T> GetAll() => dbSet.ToList();

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, bool track = true)
        {
            if (track)
            {
                return dbSet.Where(where).ToList();
            }
            else {
                return dbSet.Where(where).AsNoTracking().ToList();
            }
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> where, bool track = true)
        {
            if (track)
            {
                return dbSet.Where(where);
            }
            else {
                return dbSet.Where(where).AsNoTracking();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Query(Expression<Func<T, bool>> where, bool track = true);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
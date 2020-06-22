using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Auth.DataInfra.Repository
{
    /// <summary>
    /// Bluprint for all operations it will serve as base operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
    
}

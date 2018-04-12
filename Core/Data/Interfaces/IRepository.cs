using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core
{
    public interface IRepository<TEntity> where TEntity : class, IBaseModel
    {
        IContextProvider ContextProvider {get; set;}
        
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
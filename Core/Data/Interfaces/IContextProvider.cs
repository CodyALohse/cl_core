using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core
{
    public interface IContextProvider
    {
        void Save();

        void Add<TEntity>(TEntity entity) where TEntity : class;

        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        TEntity Get<TEntity>(int id) where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
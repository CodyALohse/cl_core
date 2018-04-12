using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace Core.Tests
{
    public class FakeContextProvider : IContextProvider
    {
        public List<Object> fakeContext = new List<Object>();


        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            this.fakeContext.Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.fakeContext.AddRange(entities);
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TEntity Get<TEntity>(int id) where TEntity : class
        {
            return (TEntity) this.fakeContext.FirstOrDefault(e => e.GetType().GetProperty("Id").GetValue(e).Equals(id));
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return this.fakeContext.Select(e => (TEntity)e).AsEnumerable();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            var entityId = entity.GetType().GetProperty("Id").GetValue(entity);
            this.fakeContext.RemoveAll(e => entityId.Equals(e.GetType().GetProperty("Id").GetValue(e)));
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            var entityIdsToRemove = entities.Select(e => e.GetType().GetProperty("Id").GetValue(e));
            this.fakeContext.RemoveAll(e => entityIdsToRemove.Any(e1 => e.GetType().GetProperty("Id").GetValue(e).Equals(e1)));
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}

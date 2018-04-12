using System;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
         void Save();

         IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseModel;
    }
}
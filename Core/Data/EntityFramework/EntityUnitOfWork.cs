using System;
using Core;

namespace Data.EntityFramework
{
    public class EntityUnitOfWork : UnitOfWork
    {
        public EntityUnitOfWork(IContextProvider contextProvider, IServiceProvider serviceProvider) : 
            base(contextProvider, serviceProvider)
        {
        }
    }
}
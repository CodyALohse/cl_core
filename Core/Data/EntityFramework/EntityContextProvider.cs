using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core;
using Data.EntityFramework.Extensions;
using Microsoft.AspNetCore.Hosting;
using Core.Data.EntityFramework;

namespace Data.EntityFramework
{
    public class EntityContextProvider<TContext> : IContextProvider where TContext : DbContext
    {
        private DbContext Context {get; set;}

        private IConfiguration Configuration;

        private readonly IHostingEnvironment HostingEnvironment;

        private IDbContextFactory<TContext> DbContextFactory;

        public EntityContextProvider(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IDbContextFactory<TContext> dbContextFactory){
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
            this.DbContextFactory = dbContextFactory;
        }

        private DbContext ApplicationContext
        {
            get
            {
                if (this.Context == null)
                {
                    this.Context = this.DbContextFactory.CreateDbContext();
                    
                    //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                    //optionsBuilder.GetDbProvider(this.HostingEnvironment.EnvironmentName, this.Configuration["ConnectionStrings:DefaultConnection"]);

                    //this.Context = new ApplicationDbContext(optionsBuilder.Options);

                    //// TODO this should be moved to the testing class instead of here
                    //// TODO not sure right now if it runs the latest migration?
                    //if (this.HostingEnvironment.EnvironmentName.Equals("IntegrationTesting"))
                    //{
                    //    // Delete the database per test. This wil probably cause horrible integration testing latency if many tests are to be run
                    //    this.Context.Database.EnsureDeleted();
                    //    this.Context.Database.Migrate();
                    //}
                }

                return this.Context;
            }
        }

        public void Save()
        {
            this.AddTimeStamp();
            this.ApplicationContext.SaveChanges();
        }

        /// <summary>
        /// Adds the CreatedOn datetimeoffset for new entities and adds or updates the ModifiedOn datetimeoffset
        /// </summary>
        private void AddTimeStamp()
        {
            var entities = this.ApplicationContext.ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreatedOn = DateTimeOffset.Now;
                }

                ((BaseModel)entity.Entity).ModifiedOn = DateTimeOffset.Now;
            }
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return this.ApplicationContext.Set<TEntity>().Where(predicate);
        }

        public TEntity Get<TEntity>(int id) where TEntity : class
        {
            return this.ApplicationContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return this.ApplicationContext.Set<TEntity>().ToList();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
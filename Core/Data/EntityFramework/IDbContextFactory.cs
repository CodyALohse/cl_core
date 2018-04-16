
namespace Core.Data.EntityFramework
{
    public interface IDbContextFactory<TContext> 
    {
        TContext CreateDbContext();
    }
}

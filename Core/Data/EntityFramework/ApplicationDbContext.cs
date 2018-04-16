using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework
{
    // This class should be extended to implement application specific DbSet's

    public class ApplicationDbContext<TContext> : DbContext where TContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<TContext> options) : base(options) { }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework
{
    // This class should be extended to implement application specific DbSet's

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<Account> Accounts { get; set; }

        //public DbSet<Institution> Institutions { get; set; }
    }
}

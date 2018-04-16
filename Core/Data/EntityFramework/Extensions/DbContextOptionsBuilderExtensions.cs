using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static void GetDbProvider(this DbContextOptionsBuilder optionsBuilder, string environment, string connectionString)
        {
            switch (environment) {
                case "IntegrationTesting":
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                case "Development":
                case "Production":
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                default:
                    break;
            }
        }
    }
}

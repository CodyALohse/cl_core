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
                case "Development": //TODO - make this more specific to postgres just incase you want to change out the DB type 
                case "Production": //TODO - make this more specific to postgres just incase you want to change out the DB type 
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                default:
                    break;
            }
        }
    }
}

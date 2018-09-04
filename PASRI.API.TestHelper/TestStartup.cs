using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PASRI.API.Persistence;

namespace PASRI.API.TestHelper
{
    public class TestStartup : Startup
    {
        private const string SqliteTestConnectionString = ":memory:";

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void AddDatabaseContext(IServiceCollection services)
        {
            // Replace default database connection with an in-memory database (SQLite)
            services.AddDbContext<PasriDbContext>(options =>
                {
                    options.UseSqlite(SqliteTestConnectionString);
                });

            // Register the database seeder
            services.AddTransient<DatabaseSeeder>();
        }

        protected override void SeedDatabaseContext(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var seeder = serviceScope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
                seeder.Seed();
            }
        }

        public static DbContextOptions<PasriDbContext> GetTestDbContextOptions()
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = SqliteTestConnectionString };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            var builder = new DbContextOptionsBuilder<PasriDbContext>();
            builder.UseSqlite(connection);
            return builder.Options;
        }
    }
}

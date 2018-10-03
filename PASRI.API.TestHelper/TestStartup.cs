using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PASRI.API.Core;

namespace PASRI.API.TestHelper
{
    /// <summary>
    /// Used as a startup class for the BaseIntegrationTestProvider and BaseUnitTestProvider
    /// that initializes the <see cref="TestUnitOfWork"/> which
    /// provides in-memory database implementation
    /// </summary>
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void AddDatabaseServices(IServiceCollection services)
        {
            // Replace default database connection with an in-memory database (SQLite)
            services.AddEntityFrameworkSqlite().AddDbContext<SqlitePasriDbContext>();

            // Inject the dependency of the IUnitOfWork to UnitOfWork to the scoped service lifetime (once per request)
            services.AddScoped<IUnitOfWork, TestUnitOfWork>();
        }
    }
}

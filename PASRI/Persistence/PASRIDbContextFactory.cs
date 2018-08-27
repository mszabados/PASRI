using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PASRI.Persistence
{
    public class PasriDbContextFactory : IDesignTimeDbContextFactory<PasriDbContext>
    {
        public PasriDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<PasriDbContext>();

            var connectionString = configuration.GetConnectionString("PasriDbContext");

            builder.UseSqlServer(connectionString);
            //builder.UseNpgsql(connectionString);

            return new PasriDbContext(builder.Options);
        }
    }
}

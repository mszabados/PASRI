using HRC.DB.Master.Core;
using HRC.DB.Master.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HRC.API.PASRI.IntegrationTests
{
    /// <summary>
    /// Used for testing only.
    /// The configuration specifies to use an in-memory SQLite database
    /// that is seeded by the <see cref="IUnitOfWork"/>
    /// </summary>
    public class SqliteMasterDbContext : MasterDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}

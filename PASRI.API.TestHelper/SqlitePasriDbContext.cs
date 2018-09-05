using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PASRI.API.Persistence;

namespace PASRI.API.TestHelper
{
    /// <summary>
    /// Used for testing only.
    /// The configuration specifies to use an in-memory SQLite database
    /// that is seeded by the <see cref="TestUnitOfWork"/>
    /// </summary>
    public class SqlitePasriDbContext : PasriDbContext
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

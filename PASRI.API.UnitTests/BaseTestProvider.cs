using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PASRI.API.Core;
using PASRI.API.Persistence;

namespace PASRI.API.UnitTests
{
    [TestFixture]
    public abstract class BaseTestProvider
    {
        protected PasriDbContext PasriDbContext;
        internal IUnitOfWork UnitOfWork;

        [SetUp]
        public void RunBeforeAllTests()
        {
            PasriDbContext = new PasriDbContext(CreateTestOptions());
            PasriDbContext.Database.OpenConnection();
            PasriDbContext.Database.EnsureCreated();

            LoadTestData();
            PasriDbContext.SaveChanges();

            UnitOfWork = new UnitOfWork(PasriDbContext);
        }

        [TearDown]
        public void RunAfterAllTests()
        {
            PasriDbContext.Database.EnsureDeleted();
            PasriDbContext.Dispose();
            UnitOfWork.Dispose();
        }

        private static DbContextOptions<PasriDbContext> CreateTestOptions()
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            var builder = new DbContextOptionsBuilder<PasriDbContext>();
            builder.UseSqlite(connection);
            return builder.Options;
        }

        protected abstract void LoadTestData();
    }
}

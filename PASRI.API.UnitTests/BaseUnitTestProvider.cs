using System.Collections;
using System.Reflection;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PASRI.API.Core;
using PASRI.API.Persistence;

namespace PASRI.API.UnitTests
{
    [TestFixture]
    public abstract class BaseUnitTestProvider
    {
        protected PasriDbContext PasriDbContext;
        public IUnitOfWork UnitOfWork;

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

        protected static void AssertPropertyValuesAreEqual(object actual, object expected)
        {
            PropertyInfo[] properties = expected.GetType().GetProperties();
            foreach (var property in properties)
            {
                var expectedValue = property.GetValue(expected, null);
                var actualValue = property.GetValue(actual, null);

                if (actualValue is IList)
                {
                    AssertListsAreEquals(property, (IList) actualValue, (IList) expectedValue);
                }
                else if (!Equals(expectedValue, actualValue))
                {
                    Assert.Fail(
                        $"Property {property.DeclaringType.Name}.{property.Name} does not match.  Expected: {expectedValue} but was: {actualValue}");
                }
            }
        }

        private static void AssertListsAreEquals(PropertyInfo property, IList actualList, IList expectedList)
        {
            if (actualList.Count != expectedList.Count)
            {
                Assert.Fail($"Property {property.PropertyType.Name}.{property.Name} does not match.  Expected IList containing {expectedList.Count} elements but contained {actualList.Count} elements");
            }

            for (int i = 0; i < actualList.Count; i++)
            {
                if (!Equals(actualList[i], expectedList[i]))
                {
                    Assert.Fail(
                        $"Property {property.DeclaringType.Name}.{property.Name} does not match.  Expected IList with element {property.Name} containing {expectedList[i]} but was: {expectedList[i]}");
                }
            }
        }

        protected abstract void LoadTestData();
    }
}

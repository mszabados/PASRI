using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PASRI.API.Core;
using PASRI.API.Persistence;
using PASRI.API.TestHelper;
using System.Collections;
using System.Reflection;

namespace PASRI.API.UnitTests
{
    [TestFixture]
    public class BaseUnitTestProvider
    {
        private PasriDbContext _context;
        public IUnitOfWork UnitOfWork;

        [SetUp]
        public void RunBeforeAllTests()
        {
            _context = new PasriDbContext(TestStartup.GetTestDbContextOptions());
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            var seeder = new DatabaseSeeder(_context);
            seeder.Seed();

            UnitOfWork = new UnitOfWork(_context);
        }

        [TearDown]
        public void RunAfterAllTests()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            UnitOfWork.Dispose();
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
    }
}

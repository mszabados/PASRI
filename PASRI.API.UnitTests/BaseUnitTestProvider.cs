using NUnit.Framework;
using PASRI.API.Core;
using PASRI.API.TestHelper;

namespace PASRI.API.UnitTests
{
    /// <summary>
    /// Creates objects necessary for the inheriting test classes to consume
    /// </summary>
    /// <example>
    /// <see cref="IUnitOfWork"/> in the UnitOfWork named field is created
    /// from <see cref="TestUnitOfWork"/>
    /// </example>
    [TestFixture]
    abstract public class BaseUnitTestProvider
    {
        public IUnitOfWork UnitOfWork;

        [SetUp]
        public void RunBeforeAllTests()
        {
            UnitOfWork = new TestUnitOfWork(new SqlitePasriDbContext());
        }

        [TearDown]
        public void RunAfterAllTests()
        {
            UnitOfWork.Dispose();
        }
    }
}

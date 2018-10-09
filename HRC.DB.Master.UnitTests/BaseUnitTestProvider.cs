using HRC.DB.Master.Core;
using NUnit.Framework;

namespace HRC.DB.Master.UnitTests
{
    /// <summary>
    /// Creates objects necessary for the inheriting test classes to consume
    /// </summary>
    /// <example>
    /// <see cref="IUnitOfWork"/> in the UnitOfWork named field is created
    /// from <see cref="TestUnitOfWork"/>
    /// </example>
    [TestFixture]
    public abstract class BaseUnitTestProvider
    {
        protected IUnitOfWork UnitOfWork;

        [SetUp]
        public void RunBeforeAllTests()
        {
            UnitOfWork = new TestUnitOfWork(new SqliteMasterDbContext());
        }

        [TearDown]
        public void RunAfterAllTests()
        {
            UnitOfWork.Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PASRI.API.Persistence;

namespace PASRI.API.TestHelper
{
    /// <summary>
    /// Unit of work class for the <see cref="SqlitePasriDbContext"/> which maintains a
    /// list of objects affected by a business transaction and coordinates the
    /// writing out of changes.
    /// </summary>
    /// <remarks>
    /// The main benefits of the repository and unit of work pattern is to create an abstraction
    /// layer between the data access/persistence layer and the business logic/application layer.
    /// It minimizes duplicate query logic and promotes testability (unit tests or TDD)
    /// 
    /// See also Patterns of Enterprise Application Architecture from Martin Fowler
    /// https://www.martinfowler.com/eaaCatalog/unitOfWork.html
    ///
    /// Repository properties should be named in plural form.
    /// </remarks>
    public class TestUnitOfWork : UnitOfWork
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        // Must be SqlitePasriDbContext for Integration tests to work
        public TestUnitOfWork(SqlitePasriDbContext context) : base(context)
        {
        }

        protected override void InitializeTestDatabaseInMemory()
        {
            Context.Database.OpenConnection();
            Context.Database.EnsureCreated();
            var seeder = new DatabaseSeeder(Context);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            seeder.Seed();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}

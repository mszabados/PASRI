using PASRI.API.Core.Domain;

namespace PASRI.API.Core.Repositories
{
    /// <summary>
    /// Extends the <see cref="IRepository{TEntity}"/> for <see cref="Person"/>
    /// to minimize duplicate fat query logic (for example: get top 10 records order by a column).
    ///
    /// This should be included in an <see cref="IUnitOfWork"/> interface for <see cref="Persistence.PasriDbContext"/>
    /// </summary>
    /// <remarks>
    /// EntityFramework (DbSet) does not meet the Repository pattern on its own due to the need
    /// to write fat queries multiple times in the application code, therefore, to minimize
    /// this query logic, it should be abstracted to a repository.
    /// </remarks>
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetByIdentificationNumber(int personIdentificationId);
    }
}

using HRC.DB.Master.Core.Domain;

namespace HRC.DB.Master.Core.Repositories
{
    /// <summary>
    /// Extends the <see cref="IRepository{TEntity}"/> for <see cref="ReferenceRace"/>
    /// to minimize duplicate fat query logic (for example: get top 10 records order by a column).
    ///
    /// This should be included in an <see cref="IUnitOfWork"/> interface for <see cref="Persistence.MasterDbContext"/>
    /// </summary>
    /// <remarks>
    /// EntityFramework (DbSet) does not meet the Repository pattern on its own due to the need
    /// to write fat queries multiple times in the application code, therefore, to minimize
    /// this query logic, it should be abstracted to a repository.
    /// </remarks>
    public interface IReferenceRaceRepository : IRepository<ReferenceRace>
    {
    }
}

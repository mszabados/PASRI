using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HRC.DB.Master.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="ReferenceRace"/>, implementing
    /// <see cref="IReferenceRaceRepository"/>, using the <see cref="MasterDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="ReferenceRace"/> objects in the database in
    /// accordance with the implemented repository methods.
    /// 
    /// This should be included in a class implementing the <see cref="Core.IUnitOfWork"/> interface
    /// for <see cref="Persistence.MasterDbContext"/>
    /// </summary>
    /// <remarks>
    /// EntityFramework (DbSet) does not meet the Repository pattern on its own due to the need
    /// to write fat queries multiple times in the application code, therefore, to minimize
    /// this query logic, it should be abstracted to a repository.
    /// </remarks>
    public class ReferenceRaceRepository : Repository<ReferenceRace>, IReferenceRaceRepository
    {
        public ReferenceRaceRepository(DbContext context)
            : base(context)
        {
        }
    }
}

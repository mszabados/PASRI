using System;
using PASRI.API.Core.Domain;
using PASRI.API.Core.Repositories;

namespace PASRI.API.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="ReferenceGenderDemographic"/>, implementing
    /// <see cref="IReferenceGenderDemographicRepository"/>, using the <see cref="PasriDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="ReferenceGenderDemographic"/> objects in the database in
    /// accordance with the implemented repository methods.
    /// 
    /// This should be included in a class implementing the <see cref="Core.IUnitOfWork"/> interface
    /// for <see cref="Persistence.PasriDbContext"/>
    /// </summary>
    /// <remarks>
    /// EntityFramework (DbSet) does not meet the Repository pattern on its own due to the need
    /// to write fat queries multiple times in the application code, therefore, to minimize
    /// this query logic, it should be abstracted to a repository.
    /// </remarks>
    public class ReferenceGenderDemographicRepository : Repository<ReferenceGenderDemographic>, IReferenceGenderDemographicRepository
    {
        public ReferenceGenderDemographicRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceGenderDemographic Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

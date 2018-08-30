﻿using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="ReferenceRaceDemographic"/>, implementing
    /// <see cref="IReferenceRaceDemographicRepository"/>, using the <see cref="PasriDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="ReferenceRaceDemographic"/> objects in the database in
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
    public class ReferenceRaceDemographicRepository : Repository<ReferenceRaceDemographic>, IReferenceRaceDemographicRepository
    {
        public ReferenceRaceDemographicRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceRaceDemographic Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

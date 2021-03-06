﻿using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HRC.DB.Master.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="Birth"/>, implementing
    /// <see cref="IBirthRepository"/>, using the <see cref="PasriDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="Birth"/> objects in the database in
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
    public class BirthRepository : Repository<Birth>, IBirthRepository
    {
        public BirthRepository(DbContext context)
            : base(context)
        {
        }

        private MasterDbContext PasriDbContext => Context as MasterDbContext;

        /// <summary>
        /// Returns an eagerly loaded <see cref="Birth"/> object for updating by the API controller
        /// </summary>
        /// <param name="birthId">Unique <see cref="Birth"/> identification number</param>
        /// <returns><see cref="Birth"/></returns>
        public Birth GetEagerLoadedBirthById(int birthId)
        {
            return PasriDbContext.Births
                .Include(b => b.StateProvince)
                .Include(b => b.Country)
                .SingleOrDefault(b => b.Id == birthId);
        }

        /// <summary>
        /// Returns an eagerly loaded <see cref="Birth"/> object for updating by the API controller
        /// </summary>
        /// <param name="personId">Unique <see cref="Person"/> identification number</param>
        /// <returns><see cref="Birth"/></returns>
        public Birth GetEagerLoadedBirthByPersonId(int personId)
        {
            return PasriDbContext.Births
                .Include(b => b.StateProvince)
                .Include(b => b.Country)
                .SingleOrDefault(b => b.PersonId == personId);
        }
    }
}

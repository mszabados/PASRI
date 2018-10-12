using System.Collections.Generic;
using System.Linq;
using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HRC.DB.Master.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="ReferencePersonnelClass"/>, implementing
    /// <see cref="IReferencePersonnelClassRepository"/>, using the <see cref="MasterDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="ReferencePersonnelClass"/> objects in the database in
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
    public class ReferencePersonnelClassRepository : Repository<ReferencePersonnelClass>, IReferencePersonnelClassRepository
    {
        public ReferencePersonnelClassRepository(DbContext context)
            : base(context)
        {
        }

        private MasterDbContext MasterDbContext => Context as MasterDbContext;

        #region Overridden standard methods

        /// <summary>
        /// Removes the dependent <see cref="ReferencePayPlan"/> collection
        /// before removing the <see cref="ReferencePersonnelClass"/>
        /// to comply with the foreign-key which is not cascading
        /// </summary>
        public override void Remove(ReferencePersonnelClass personnelClass)
        {
            var queryPayPlans = MasterDbContext.ReferencePayPlans.AsQueryable();
            queryPayPlans = queryPayPlans.Where(sp => sp.PersonnelClassId == personnelClass.Id);
            MasterDbContext.ReferencePayPlans.RemoveRange(queryPayPlans.ToList());
            MasterDbContext.ReferencePersonnelClasses.Remove(personnelClass);
        }

        /// <summary>
        /// Removes the dependent <see cref="ReferencePayPlan"/> collections
        /// before removing the <see cref="ReferencePersonnelClass"/> collection
        /// to comply with the foreign-key which is not cascading
        /// </summary>
        public override void RemoveRange(IEnumerable<ReferencePersonnelClass> personnelClasses)
        {
            var referencePersonnelClasses = personnelClasses.ToList();
            foreach (var personnelClass in referencePersonnelClasses)
            {
                var queryPayPlans = MasterDbContext.ReferencePayPlans.AsQueryable();
                queryPayPlans = queryPayPlans.Where(sp => sp.PersonnelClassId == personnelClass.Id);
                MasterDbContext.ReferencePayPlans.RemoveRange(queryPayPlans.ToList());
            }

            MasterDbContext.ReferencePersonnelClasses.RemoveRange(referencePersonnelClasses);
        }

        #endregion
    }
}

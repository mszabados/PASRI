using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HRC.DB.Master.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="ReferenceServiceBranch"/>, implementing
    /// <see cref="IReferenceServiceBranchRepository"/>, using the <see cref="MasterDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="ReferenceServiceBranch"/> objects in the database in
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
    public class ReferenceServiceBranchRepository : Repository<ReferenceServiceBranch>, IReferenceServiceBranchRepository
    {
        public ReferenceServiceBranchRepository(DbContext context)
            : base(context)
        {
        }

        private MasterDbContext MasterDbContext => Context as MasterDbContext;

        #region Overridden standard methods

        /// <summary>
        /// Removes the dependent <see cref="ReferenceServiceBranchComponent"/> collection
        /// before removing the <see cref="ReferenceServiceBranch"/>
        /// to comply with the foreign-key which is not cascading
        /// </summary>
        public override void Remove(ReferenceServiceBranch serviceBranch)
        {
            var queryServiceBranchComponents = MasterDbContext.ReferenceServiceBranchComponents.AsQueryable();
            queryServiceBranchComponents = queryServiceBranchComponents.Where(sp => sp.ServiceBranchId == serviceBranch.Id);
            MasterDbContext.ReferenceServiceBranchComponents.RemoveRange(queryServiceBranchComponents.ToList());
            MasterDbContext.ReferenceServiceBranches.Remove(serviceBranch);
        }

        /// <summary>
        /// Removes the dependent <see cref="ReferenceServiceBranchComponent"/> collections
        /// before removing the <see cref="ReferenceServiceBranch"/> collection
        /// to comply with the foreign-key which is not cascading
        /// </summary>
        public override void RemoveRange(IEnumerable<ReferenceServiceBranch> serviceBranches)
        {
            var referenceServiceBranches = serviceBranches.ToList();
            foreach (var serviceBranch in referenceServiceBranches)
            {
                var queryServiceBranchComponents = MasterDbContext.ReferenceServiceBranchComponents.AsQueryable();
                queryServiceBranchComponents = queryServiceBranchComponents.Where(sp => sp.ServiceBranchId == serviceBranch.Id);
                MasterDbContext.ReferenceServiceBranchComponents.RemoveRange(queryServiceBranchComponents.ToList());
            }

            MasterDbContext.ReferenceServiceBranches.RemoveRange(referenceServiceBranches);
        }

        #endregion
    }
}

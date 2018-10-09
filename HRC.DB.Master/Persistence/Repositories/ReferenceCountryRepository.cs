using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HRC.DB.Master.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="ReferenceCountry"/>, implementing
    /// <see cref="IReferenceCountryRepository"/>, using the <see cref="MasterDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="ReferenceCountry"/> objects in the database in
    /// accordance with the implemented repository methods.
    /// 
    /// This should be included in a class implementing the <see cref="Core.IUnitOfWork"/> interface
    /// for <see cref="Persistence.MasterDbContext"/>
    /// </summary>
    /// /// <remarks>
    /// EntityFramework (DbSet) does not meet the Repository pattern on its own due to the need
    /// to write fat queries multiple times in the application code, therefore, to minimize
    /// this query logic, it should be abstracted to a repository.
    /// </remarks>
    public class ReferenceCountryRepository : Repository<ReferenceCountry>, IReferenceCountryRepository
    {
        public ReferenceCountryRepository(DbContext context)
            : base(context)
        {
        }

        private MasterDbContext MasterDbContext => Context as MasterDbContext;

        #region Overridden standard methods

        /// <summary>
        /// Removes the dependent state/provinces before removing the country
        /// to comply with the foreign-key which is not cascading
        /// </summary>
        public override void Remove(ReferenceCountry country)
        {
            var queryStateProvinces = MasterDbContext.ReferenceStateProvinces.AsQueryable();
            queryStateProvinces = queryStateProvinces.Where(sp => sp.CountryId == country.Id);
            MasterDbContext.ReferenceStateProvinces.RemoveRange(queryStateProvinces.ToList());
            MasterDbContext.ReferenceCountries.Remove(country);
        }

        /// <summary>
        /// Removes the dependent state/provinces before removing the countries
        /// to comply with the foreign-key which is not cascading
        /// </summary>
        public override void RemoveRange(IEnumerable<ReferenceCountry> countries)
        {
            foreach (var country in countries)
            {
                var queryStateProvinces = MasterDbContext.ReferenceStateProvinces.AsQueryable();
                queryStateProvinces = queryStateProvinces.Where(sp => sp.CountryId == country.Id);
                MasterDbContext.ReferenceStateProvinces.RemoveRange(queryStateProvinces.ToList());
            }

            MasterDbContext.ReferenceCountries.RemoveRange(countries);
        }

        #endregion
    }
}

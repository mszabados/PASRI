using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PASRI.API.Core.Domain;
using PASRI.API.Core.Repositories;

namespace PASRI.API.Persistence.Repositories
{
    /// <summary>
    /// Extends the <see cref="Repository{TEntity}"/> for <see cref="Person"/>, implementing
    /// <see cref="IPersonRepository"/>, using the <see cref="PasriDbContext"/> to get,
    /// find with predicate, add, or remove <see cref="Person"/> objects in the database in
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
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context)
            : base(context)
        {
        }

        private PasriDbContext PasriDbContext => Context as PasriDbContext;

        #region Constants for dynamically selecting information about a Person
        /*
        1. All constants must be name with Info due to the base.IncludeInfoConstantSuffix
            and its corresponding helper method base.GetAllIncludeInfoConstants()
        2. Update the includeList parameter's example XML documentation directly
            so the API's documentation builder can describe the optional parameter 
            for including info
        */

        private const string BirthInfo = nameof(BirthInfo);
        private const string DemographicInfo = nameof(DemographicInfo);
        
        #endregion

        /// <summary>
        /// Returns an eager loaded <see cref="Person"/> object
        /// </summary>
        /// <param name="personId">Unique <see cref="Person"/> identification number</param>
        /// <returns><see cref="Person"/></returns>
        public Person GetEagerLoadedPerson(int personId) => 
            Search(personId, GetAllIncludeInfoConstants()).SingleOrDefault();

        /// <summary>
        /// Searches and selects from the database a <see cref="Person"/> collection
        /// based on input parameters
        /// </summary>
        /// <param name="personId">Optional <see cref="Person"/> identification number</param>
        /// <param name="includeInfo">Optional list of include objects corresponding with
        /// the IncludeInfo constants of <see cref="PersonRepository"/></param>
        /// <returns><see cref="List{Person}"/></returns>
        public List<Person> Search(int? personId, 
            List<string> includeInfo)
        {
            var persons = PasriDbContext.Persons.AsQueryable();

            if (personId.HasValue)
            {
                persons = persons.Where(p => p.Id == personId);
            }

            if (includeInfo.Contains(BirthInfo))
            {
                persons = persons.Include(p => p.Birth);
                persons = persons.Include(p => p.Birth.StateProvince);
                persons = persons.Include(p => p.Birth.Country);
            }

            persons = persons.Include(p => p.Suffix);

            return persons.ToList();
        }
    }
}

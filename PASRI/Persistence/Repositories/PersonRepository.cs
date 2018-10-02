using System.Linq;
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
        public PersonRepository(PasriDbContext context)
            : base(context)
        {
        }

        public PasriDbContext PasriDbContext
        {
            get { return Context as PasriDbContext; }
        }

        /// <summary>
        /// Returns an eagerly loaded <see cref="Person"/> object for updating by the API controller
        /// </summary>
        /// <param name="personId">Unique <see cref="Person"/> identification number</param>
        /// <returns><see cref="Person"/></returns>
        public Person GetEagerLoadedPerson(int personId)
        {            
            return PasriDbContext.Persons
                .Include(p => p.Birth)
                .Include(p => p.Birth.StateProvince)
                .Include(p => p.Birth.Country)
                .Include(p => p.Suffix)
                .SingleOrDefault(p => p.Id == personId);
            
        }
    }
}

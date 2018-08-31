using Microsoft.EntityFrameworkCore;
using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System.Linq;

namespace PASRI.Persistence.Repositories
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

        public Person GetByIdentificationNumber(int personIdentificationId)
        {
            return PasriDbContext.Persons
                .Include(p => p.PersonIdentifications)
                    .ThenInclude(pi => pi.PersonNameIdentifications)
                        .ThenInclude(pni => pni.PersonLegalNameIdentifications)
                .SingleOrDefault(pi => pi.Id == personIdentificationId);
        }
    }
}

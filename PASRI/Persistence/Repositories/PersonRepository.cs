using Microsoft.EntityFrameworkCore;
using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System.Linq;

namespace PASRI.Persistence.Repositories
{
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

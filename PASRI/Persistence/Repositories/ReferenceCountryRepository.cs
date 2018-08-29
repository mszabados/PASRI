using System;
using System.Collections.Generic;
using PASRI.Core.Domain;
using PASRI.Core.Repositories;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceCountryRepository : Repository<ReferenceCountry>, IReferenceCountryRepository
    {
        public ReferenceCountryRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new void Remove(ReferenceCountry entity)
        {
            throw new NotImplementedException();
        }

        public new void RemoveRange(IEnumerable<ReferenceCountry> entities)
        {
            throw new NotImplementedException();
        }
    }
}

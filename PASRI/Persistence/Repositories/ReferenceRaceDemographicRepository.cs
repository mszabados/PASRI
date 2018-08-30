using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
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

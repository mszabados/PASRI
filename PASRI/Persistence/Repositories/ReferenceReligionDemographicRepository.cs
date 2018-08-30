using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceReligionDemographicRepository : Repository<ReferenceReligionDemographic>, IReferenceReligionDemographicRepository
    {
        public ReferenceReligionDemographicRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceReligionDemographic Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

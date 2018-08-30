using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceGenderDemographicRepository : Repository<ReferenceGenderDemographic>, IReferenceGenderDemographicRepository
    {
        public ReferenceGenderDemographicRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceGenderDemographic Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

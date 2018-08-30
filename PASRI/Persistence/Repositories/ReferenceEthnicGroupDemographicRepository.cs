using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceEthnicGroupDemographicRepository : Repository<ReferenceEthnicGroupDemographic>, IReferenceEthnicGroupDemographicRepository
    {
        public ReferenceEthnicGroupDemographicRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceEthnicGroupDemographic Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

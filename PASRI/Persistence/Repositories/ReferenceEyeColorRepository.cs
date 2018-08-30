using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceEyeColorRepository : Repository<ReferenceEyeColor>, IReferenceEyeColorRepository
    {
        public ReferenceEyeColorRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceEyeColor Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

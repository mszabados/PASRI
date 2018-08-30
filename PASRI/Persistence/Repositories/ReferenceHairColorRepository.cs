using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceHairColorRepository : Repository<ReferenceHairColor>, IReferenceHairColorRepository
    {
        public ReferenceHairColorRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceHairColor Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

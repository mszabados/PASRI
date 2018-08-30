using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceStateRepository : Repository<ReferenceState>, IReferenceStateRepository
    {
        public ReferenceStateRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceState Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

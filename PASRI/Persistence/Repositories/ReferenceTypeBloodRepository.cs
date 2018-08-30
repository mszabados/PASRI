using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceTypeBloodRepository : Repository<ReferenceTypeBlood>, IReferenceTypeBloodRepository
    {
        public ReferenceTypeBloodRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceTypeBlood Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

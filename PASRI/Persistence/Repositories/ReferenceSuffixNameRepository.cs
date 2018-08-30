using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceSuffixNameRepository : Repository<ReferenceSuffixName>, IReferenceSuffixNameRepository
    {
        public ReferenceSuffixNameRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceSuffixName Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

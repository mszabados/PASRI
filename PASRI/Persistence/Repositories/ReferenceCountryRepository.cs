﻿using PASRI.Core.Domain;
using PASRI.Core.Repositories;
using System;

namespace PASRI.Persistence.Repositories
{
    public class ReferenceCountryRepository : Repository<ReferenceCountry>, IReferenceCountryRepository
    {
        public ReferenceCountryRepository(PasriDbContext context)
            : base(context)
        {
        }

        public new ReferenceCountry Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
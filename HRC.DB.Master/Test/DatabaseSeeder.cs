﻿using HRC.DB.Master.Persistence;
using System.Threading.Tasks;

namespace HRC.DB.Master.Test
{
    /// <summary>
    /// Exposes a method to see the <see cref="MasterDbContext"/> with test data
    /// defined in the <see cref="PreDefinedData"/> class.
    /// </summary>
    public class DatabaseSeeder
    {
        private readonly MasterDbContext _context;

        public DatabaseSeeder(MasterDbContext context)
        {
            _context = context;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Add all of the pre-defined data into the <see cref="MasterDbContext"/>
        /// </summary>
        public async Task Seed()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            _context.Persons.AddRange(PreDefinedData.Persons);
            _context.Births.AddRange(PreDefinedData.Births);
            _context.ReferenceAccessionSources.AddRange(PreDefinedData.ReferenceAccessionSources);
            _context.ReferenceBasesForUsCitizenship.AddRange(PreDefinedData.ReferenceBasesForUsCitizenship);
            _context.ReferenceBloodTypes.AddRange(PreDefinedData.ReferenceBloodTypes);
            _context.ReferenceCountries.AddRange(PreDefinedData.ReferenceCountries);
            _context.ReferenceEthnicGroups.AddRange(PreDefinedData.ReferenceEthnicGroups);
            _context.ReferenceEyeColors.AddRange(PreDefinedData.ReferenceEyeColors);
            _context.ReferenceGenders.AddRange(PreDefinedData.ReferenceGenders);
            _context.ReferenceHairColors.AddRange(PreDefinedData.ReferenceHairColors);
            _context.ReferenceMarriages.AddRange(PreDefinedData.ReferenceMarriages);
            _context.ReferenceMilSvcCitizenshipQuals.AddRange(PreDefinedData.ReferenceMilSvcCitizenshipQuals);
            _context.ReferenceNameSuffixes.AddRange(PreDefinedData.ReferenceNameSuffixes);
            _context.ReferencePayPlans.AddRange(PreDefinedData.ReferencePayPlans);
            _context.ReferencePersonnelClasses.AddRange(PreDefinedData.ReferencePersonnelClasses);
            _context.ReferenceRaces.AddRange(PreDefinedData.ReferenceRaces);
            _context.ReferenceReligions.AddRange(PreDefinedData.ReferenceReligions);
            _context.ReferenceServiceBranches.AddRange(PreDefinedData.ReferenceServiceBranches);
            _context.ReferenceServiceBranchComponents.AddRange(PreDefinedData.ReferenceServiceBranchComponents);
            _context.ReferenceSsnVerifications.AddRange(PreDefinedData.ReferenceSsnVerifications);
            _context.ReferenceStateProvinces.AddRange(PreDefinedData.ReferenceStateProvinces);

            _context.SaveChanges();
        }
    }
}

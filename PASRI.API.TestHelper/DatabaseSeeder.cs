using System.Threading.Tasks;
using PASRI.API.Persistence;

namespace PASRI.API.TestHelper
{
    /// <summary>
    /// Exposes a method to see the <see cref="PasriDbContext"/> with test data
    /// defined in the <see cref="PreDefinedData"/> class.
    /// </summary>
    public class DatabaseSeeder
    {
        private readonly PasriDbContext _context;

        public DatabaseSeeder(PasriDbContext context)
        {
            _context = context;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Add all of the pre-defined data into the <see cref="PasriDbContext"/>
        /// </summary>
        public async Task Seed()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            _context.Persons.AddRange(PreDefinedData.Persons);
            _context.Births.AddRange(PreDefinedData.Births);
            _context.ReferenceBloodTypes.AddRange(PreDefinedData.ReferenceBloodTypes);
            _context.ReferenceCountries.AddRange(PreDefinedData.ReferenceCountries);
            _context.ReferenceEthnicGroups.AddRange(PreDefinedData.ReferenceEthnicGroups);
            _context.ReferenceEyeColors.AddRange(PreDefinedData.ReferenceEyeColors);
            _context.ReferenceGenders.AddRange(PreDefinedData.ReferenceGenders);
            _context.ReferenceHairColors.AddRange(PreDefinedData.ReferenceHairColors);
            _context.ReferenceNameSuffixes.AddRange(PreDefinedData.ReferenceNameSuffixes);
            _context.ReferenceRaces.AddRange(PreDefinedData.ReferenceRaces);
            _context.ReferenceReligions.AddRange(PreDefinedData.ReferenceReligions);
            _context.ReferenceStates.AddRange(PreDefinedData.ReferenceStateProvinces);

            _context.SaveChanges();
        }
    }
}

using System.Threading.Tasks;
using PASRI.API.Persistence;

namespace PASRI.API.TestHelper
{
    public class DatabaseSeeder
    {
        private readonly PasriDbContext _context;

        public DatabaseSeeder(PasriDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add all of the pre-defined data into the context
        /// </summary>
        public async Task Seed()
        {
            _context.ReferenceBloodTypes.AddRange(PreDefinedData.ReferenceBloodTypes);
            _context.ReferenceCountries.AddRange(PreDefinedData.ReferenceCountries);
            _context.ReferenceEthnicGroupDemographics.AddRange(PreDefinedData.ReferenceEthnicGroupDemographics);
            _context.ReferenceEyeColors.AddRange(PreDefinedData.ReferenceEyeColors);
            _context.ReferenceGenderDemographics.AddRange(PreDefinedData.ReferenceGenderDemographics);
            _context.ReferenceHairColors.AddRange(PreDefinedData.ReferenceHairColors);
            _context.ReferenceRaceDemographics.AddRange(PreDefinedData.ReferenceRaceDemographics);
            _context.ReferenceReligionDemographics.AddRange(PreDefinedData.ReferenceReligionDemographics);
            _context.ReferenceStates.AddRange(PreDefinedData.ReferenceStates);
            _context.ReferenceSuffixNames.AddRange(PreDefinedData.ReferenceSuffixNames);

            _context.SaveChanges();
        }
    }
}

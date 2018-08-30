using PASRI.Core;
using PASRI.Core.Repositories;
using PASRI.Persistence.Repositories;

namespace PASRI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PasriDbContext _context;

        public UnitOfWork(PasriDbContext context)
        {
            _context = context;
            Persons = new PersonRepository(_context);
            ReferenceCountries = new ReferenceCountryRepository(_context);
            ReferenceEthnicGroupDemographics = new ReferenceEthnicGroupDemographicRepository(_context);
            ReferenceEyeColors = new ReferenceEyeColorRepository(_context);
            ReferenceGenderDemographics = new ReferenceGenderDemographicRepository(_context);
            ReferenceHairColors = new ReferenceHairColorRepository(_context);
            ReferenceRaceDemographics = new ReferenceRaceDemographicRepository(_context);
            ReferenceReligionDemographics = new ReferenceReligionDemographicRepository(_context);
            ReferenceStates = new ReferenceStateRepository(_context);
            ReferenceSuffixNames = new ReferenceSuffixNameRepository(_context);
            ReferenceTypeBloods = new ReferenceTypeBloodRepository(_context);
        }

        public IPersonRepository Persons { get; private set; }
        public IReferenceCountryRepository ReferenceCountries { get; private set; }
        public IReferenceEthnicGroupDemographicRepository ReferenceEthnicGroupDemographics { get; private set; }
        public IReferenceEyeColorRepository ReferenceEyeColors { get; private set; }
        public IReferenceGenderDemographicRepository ReferenceGenderDemographics { get; private set; }
        public IReferenceHairColorRepository ReferenceHairColors { get; private set; }
        public IReferenceRaceDemographicRepository ReferenceRaceDemographics { get; private set; }
        public IReferenceReligionDemographicRepository ReferenceReligionDemographics { get; private set; }
        public IReferenceStateRepository ReferenceStates { get; private set; }
        public IReferenceSuffixNameRepository ReferenceSuffixNames { get; private set; }
        public IReferenceTypeBloodRepository ReferenceTypeBloods { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

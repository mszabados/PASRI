using PASRI.API.Core;
using PASRI.API.Core.Repositories;
using PASRI.API.Persistence.Repositories;

namespace PASRI.API.Persistence
{
    /// <summary>
    /// Unit of work class for the PasriDbContext maintains a list of objects affected by a
    /// business transaction and coordinates the writing out of changes.
    /// </summary>
    /// <remarks>
    /// The main benefits of the repository and unit of work pattern is to create an abstraction
    /// layer between the data access/persistence layer and the business logic/application layer.
    /// It minimizes duplicate query logic and promotes testability (unit tests or TDD)
    /// 
    /// See also Patterns of Enterprise Application Architecture from Martin Fowler
    /// https://www.martinfowler.com/eaaCatalog/unitOfWork.html
    ///
    /// Repository properties should be named in plural form.
    /// </remarks>
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
            ReferenceTypeBloods = new ReferenceBloodTypeRepository(_context);
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
        public IReferenceBloodTypeRepository ReferenceTypeBloods { get; private set; }

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

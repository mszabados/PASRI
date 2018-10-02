using System;
using Microsoft.EntityFrameworkCore;
using PASRI.API.Core;
using PASRI.API.Core.Repositories;
using PASRI.API.Persistence.Repositories;

namespace PASRI.API.Persistence
{
    /// <summary>
    /// Unit of work class for the <see cref="PasriDbContext"/> which maintains a
    /// list of objects affected by a business transaction and coordinates the
    /// writing out of changes.
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
        protected PasriDbContext _context;

        public UnitOfWork(PasriDbContext context)
        {
            _context = context;

            if (Environment.GetEnvironmentVariable("DOCKER_ENVIRONMENT") == "Development")
            {
                _context.Database.OpenConnection();
                _context.Database.Migrate();
            }

            InitializeTestDatabaseInMemory();

            Persons = new PersonRepository(_context);
            Births = new BirthRepository(_context);
            ReferenceCountries = new ReferenceCountryRepository(_context);
            ReferenceEthnicGroupDemographics = new ReferenceEthnicGroupDemographicRepository(_context);
            ReferenceEyeColors = new ReferenceEyeColorRepository(_context);
            ReferenceGenderDemographics = new ReferenceGenderDemographicRepository(_context);
            ReferenceHairColors = new ReferenceHairColorRepository(_context);
            ReferenceRaceDemographics = new ReferenceRaceDemographicRepository(_context);
            ReferenceReligionDemographics = new ReferenceReligionDemographicRepository(_context);
            ReferenceStateProvinces = new ReferenceStateProvinceRepository(_context);
            ReferenceNameSuffixes = new ReferenceNameSuffixRepository(_context);
            ReferenceBloodTypes = new ReferenceBloodTypeRepository(_context);
        }


        public IPersonRepository Persons { get; protected set; }
        public IBirthRepository Births { get; protected set; }
        public IReferenceCountryRepository ReferenceCountries { get; protected set; }
        public IReferenceEthnicGroupDemographicRepository ReferenceEthnicGroupDemographics { get; protected set; }
        public IReferenceEyeColorRepository ReferenceEyeColors { get; protected set; }
        public IReferenceGenderDemographicRepository ReferenceGenderDemographics { get; protected set; }
        public IReferenceHairColorRepository ReferenceHairColors { get; protected set; }
        public IReferenceRaceDemographicRepository ReferenceRaceDemographics { get; protected set; }
        public IReferenceReligionDemographicRepository ReferenceReligionDemographics { get; protected set; }
        public IReferenceStateProvinceRepository ReferenceStateProvinces { get; protected set; }
        public IReferenceNameSuffixRepository ReferenceNameSuffixes { get; protected set; }
        public IReferenceBloodTypeRepository ReferenceBloodTypes { get; protected set; }

        /// <summary>
        /// A derived unit of work class can initialize and
        /// seed the database for testing.  This method is called
        /// in this <see cref="UnitOfWork"/> constructor
        /// </summary>
        virtual protected void InitializeTestDatabaseInMemory()
        {
        }

        /// <summary>
        /// Saves the changes into the context, and subsequently into the defined database
        /// </summary>
        /// <returns></returns>
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

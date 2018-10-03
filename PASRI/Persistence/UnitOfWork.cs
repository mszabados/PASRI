using System;
using System.Diagnostics.CodeAnalysis;
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
        protected readonly PasriDbContext Context;

        public UnitOfWork(PasriDbContext context)
        {
            Context = context;

            // ReSharper disable once VirtualMemberCallInConstructor
            InitializeDatabaseSchema();

            Persons = new PersonRepository(Context);
            Births = new BirthRepository(Context);
            ReferenceCountries = new ReferenceCountryRepository(Context);
            ReferenceEthnicGroupDemographics = new ReferenceEthnicGroupDemographicRepository(Context);
            ReferenceEyeColors = new ReferenceEyeColorRepository(Context);
            ReferenceGenderDemographics = new ReferenceGenderDemographicRepository(Context);
            ReferenceHairColors = new ReferenceHairColorRepository(Context);
            ReferenceRaceDemographics = new ReferenceRaceDemographicRepository(Context);
            ReferenceReligionDemographics = new ReferenceReligionDemographicRepository(Context);
            ReferenceStateProvinces = new ReferenceStateProvinceRepository(Context);
            ReferenceNameSuffixes = new ReferenceNameSuffixRepository(Context);
            ReferenceBloodTypes = new ReferenceBloodTypeRepository(Context);
        }


        public IPersonRepository Persons { get; }
        public IBirthRepository Births { get; }
        public IReferenceCountryRepository ReferenceCountries { get; }
        public IReferenceEthnicGroupDemographicRepository ReferenceEthnicGroupDemographics { get; }
        public IReferenceEyeColorRepository ReferenceEyeColors { get; }
        public IReferenceGenderDemographicRepository ReferenceGenderDemographics { get; }
        public IReferenceHairColorRepository ReferenceHairColors { get; }
        public IReferenceRaceDemographicRepository ReferenceRaceDemographics { get; }
        public IReferenceReligionDemographicRepository ReferenceReligionDemographics { get; }
        public IReferenceStateProvinceRepository ReferenceStateProvinces { get; }
        public IReferenceNameSuffixRepository ReferenceNameSuffixes { get; }
        public IReferenceBloodTypeRepository ReferenceBloodTypes { get; }

        /// <summary>
        /// A derived unit of work class can initialize and
        /// seed the database for testing.  This method is called
        /// in this <see cref="UnitOfWork"/> constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected virtual void InitializeDatabaseSchema()
        {
            if (Environment.GetEnvironmentVariable("DOCKER_ENVIRONMENT") == "Development")
            {
                Context.Database.OpenConnection();
                Context.Database.Migrate();
            }
        }

        /// <summary>
        /// Saves the changes into the context, and subsequently into the defined database
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

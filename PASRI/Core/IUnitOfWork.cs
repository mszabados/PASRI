using System;
using PASRI.API.Core.Repositories;

namespace PASRI.API.Core
{
    /// <summary>
    /// Unit of work interface to expose a list of objects affected by a business transaction
    /// for the implementing class to coordinate the writing out of changes (or testing)
    /// </summary>
    /// <remarks>
    /// The main benefits of the repository and unit of work pattern is to create an abstraction
    /// layer between the data access/persistence layer and the business logic/application layer.
    /// It minimizes duplicate query logic and promotes testability (unit tests or TDD)
    /// 
    /// See also Patterns of Enterprise Application Architecture from Martin Fowler
    /// https://www.martinfowler.com/eaaCatalog/unitOfWork.html
    /// </remarks>
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository Persons { get;  }
        IBirthRepository Births { get; }
        IReferenceCountryRepository ReferenceCountries { get; }
        IReferenceEthnicGroupRepository ReferenceEthnicGroups { get; }
        IReferenceEyeColorRepository ReferenceEyeColors { get; }
        IReferenceGenderRepository ReferenceGenders { get; }
        IReferenceHairColorRepository ReferenceHairColors { get; }
        IReferenceRaceRepository ReferenceRaces { get; }
        IReferenceReligionRepository ReferenceReligions { get; }
        IReferenceStateProvinceRepository ReferenceStateProvinces { get; }
        IReferenceNameSuffixRepository ReferenceNameSuffixes { get; }
        IReferenceBloodTypeRepository ReferenceBloodTypes { get; }

        int Complete();
    }
}

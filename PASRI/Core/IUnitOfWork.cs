using PASRI.Core.Repositories;
using System;

namespace PASRI.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository Persons { get; }
        IReferenceCountryRepository ReferenceCountries { get; }
        IReferenceEthnicGroupDemographicRepository ReferenceEthnicGroupDemographics { get; }
        IReferenceEyeColorRepository ReferenceEyeColors { get; }
        IReferenceGenderDemographicRepository ReferenceGenderDemographics { get; }
        IReferenceHairColorRepository ReferenceHairColors { get; }
        IReferenceRaceDemographicRepository ReferenceRaceDemographics { get; }
        IReferenceReligionDemographicRepository ReferenceReligionDemographics { get; }
        IReferenceStateRepository ReferenceStates { get; }
        IReferenceSuffixNameRepository ReferenceSuffixNames { get; }
        IReferenceTypeBloodRepository ReferenceTypeBloods { get; }

        int Complete();
    }
}

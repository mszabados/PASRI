using AutoMapper;
using PASRI.API.Core.Domain;
using PASRI.API.Dtos;

namespace PASRI.API
{
    /// <summary>
    /// Creates the AutoMapper profile for the bidirectional mapping of
    /// domain model objects and their respective DTO objects
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReferenceCountry, ReferenceCountryDto>();
            CreateMap<ReferenceCountryDto, ReferenceCountry>();
            CreateMap<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>();
            CreateMap<ReferenceEthnicGroupDemographicDto, ReferenceEthnicGroupDemographic>();
            CreateMap<ReferenceEyeColor, ReferenceEyeColorDto>();
            CreateMap<ReferenceEyeColorDto, ReferenceEyeColor>();
            CreateMap<ReferenceGenderDemographic, ReferenceGenderDemographicDto>();
            CreateMap<ReferenceGenderDemographicDto, ReferenceGenderDemographic>();
            CreateMap<ReferenceHairColor, ReferenceHairColorDto>();
            CreateMap<ReferenceHairColorDto, ReferenceHairColor>();
            CreateMap<ReferenceRaceDemographic, ReferenceRaceDemographicDto>();
            CreateMap<ReferenceRaceDemographicDto, ReferenceRaceDemographic>();
            CreateMap<ReferenceReligionDemographic, ReferenceReligionDemographicDto>();
            CreateMap<ReferenceReligionDemographicDto, ReferenceReligionDemographic>();
            CreateMap<ReferenceState, ReferenceStateDto>();
            CreateMap<ReferenceStateDto, ReferenceState>();
            CreateMap<ReferenceSuffixName, ReferenceSuffixNameDto>();
            CreateMap<ReferenceSuffixNameDto, ReferenceSuffixName>();
            CreateMap<ReferenceBloodType, ReferenceBloodTypeDto>();
            CreateMap<ReferenceBloodTypeDto, ReferenceBloodType>();
        }
    }
}

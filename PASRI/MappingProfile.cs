using AutoMapper;
using PASRI.Core.Domain;
using PASRI.Dtos;

namespace PASRI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReferenceCountry, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceCountry>();
            CreateMap<ReferenceEthnicGroupDemographic, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceEthnicGroupDemographic>();
            CreateMap<ReferenceEyeColor, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceEyeColor>();
            CreateMap<ReferenceGenderDemographic, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceGenderDemographic>();
            CreateMap<ReferenceHairColor, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceHairColor>();
            CreateMap<ReferenceRaceDemographic, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceRaceDemographic>();
            CreateMap<ReferenceReligionDemographic, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceReligionDemographic>();
            CreateMap<ReferenceState, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceState>();
            CreateMap<ReferenceSuffixName, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceSuffixName>();
            CreateMap<ReferenceTypeBlood, ReferenceBaseDto>();
            CreateMap<ReferenceBaseDto, ReferenceTypeBlood>();
        }
    }
}

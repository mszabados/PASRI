using AutoMapper;
using PASRI.Core.Domain;
using PASRI.Dtos;

namespace PASRI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReferenceCountry, ReferenceCountryDto>();
            CreateMap<ReferenceCountryDto, ReferenceCountry>();
        }
    }
}

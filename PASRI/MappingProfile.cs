using System;
using System.Linq.Expressions;
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
            // Straight DTO to domain model maps
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<ReferenceBloodType, ReferenceBloodTypeDto>().ReverseMap();
            CreateMap<ReferenceCountry, ReferenceCountryDto>().ReverseMap();
            CreateMap<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>().ReverseMap();
            CreateMap<ReferenceEyeColor, ReferenceEyeColorDto>().ReverseMap();
            CreateMap<ReferenceGenderDemographic, ReferenceGenderDemographicDto>().ReverseMap();
            CreateMap<ReferenceHairColor, ReferenceHairColorDto>().ReverseMap();
            CreateMap<ReferenceNameSuffix, ReferenceNameSuffixDto>().ReverseMap();
            CreateMap<ReferenceRaceDemographic, ReferenceRaceDemographicDto>().ReverseMap();
            CreateMap<ReferenceReligionDemographic, ReferenceReligionDemographicDto>().ReverseMap();
            CreateMap<ReferenceStateProvince, ReferenceStateProvinceDto>().ReverseMap();
        }
    }

    public static class MappingHelper
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}

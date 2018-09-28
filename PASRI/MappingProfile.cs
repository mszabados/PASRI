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
            // Straight object copying without Id
            CreateMap<Person, Person>().ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Birth, Birth>().ForMember(d => d.Id, opt => opt.Ignore());
            
            // Straight DTO to domain model maps
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

            // Complex mapping for nested types
            CreateMap<Person, PersonDto>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Birth.Date))
                .ForMember(d => d.BirthCity, opt => opt.MapFrom(src => src.Birth.City))
                .ForMember(d => d.BirthStateProvinceCode, opt => opt.MapFrom(src => src.Birth.StateProvince.Code))
                .ForMember(d => d.BirthCountryCode, opt => opt.MapFrom(src => src.Birth.Country.Code))
                .ReverseMap()
                .ForPath(s => s.Birth.Date, opt => opt.MapFrom(src => src.BirthDate))
                .ForPath(s => s.Birth.City, opt => opt.MapFrom(src => src.BirthCity))
                .ForPath(s => s.Birth.StateProvince.Code, opt => opt.MapFrom(src => src.BirthStateProvinceCode))
                .ForPath(s => s.Birth.Country.Code, opt => opt.MapFrom(src => src.BirthCountryCode));
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

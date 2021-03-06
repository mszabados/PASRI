﻿using AutoMapper;
using HRC.API.PASRI.Dtos;
using HRC.DB.Master.Core.Domain;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace HRC.API.PASRI
{
    /// <inheritdoc />
    /// <summary>
    /// Creates the AutoMapper profile for the bidirectional mapping of
    /// domain model objects and their respective DTO objects
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Straight DTO to domain model maps
            CreateMap<ReferenceBloodType, ReferenceBloodTypeDto>().ReverseMap();
            CreateMap<ReferenceCountry, ReferenceCountryDto>().ReverseMap();
            CreateMap<ReferenceEthnicGroup, ReferenceEthnicGroupDto>().ReverseMap();
            CreateMap<ReferenceEyeColor, ReferenceEyeColorDto>().ReverseMap();
            CreateMap<ReferenceGender, ReferenceGenderDto>().ReverseMap();
            CreateMap<ReferenceHairColor, ReferenceHairColorDto>().ReverseMap();
            CreateMap<ReferenceNameSuffix, ReferenceNameSuffixDto>().ReverseMap();
            CreateMap<ReferenceRace, ReferenceRaceDto>().ReverseMap();
            CreateMap<ReferenceReligion, ReferenceReligionDto>().ReverseMap();
            

            // Complex mapping for nested types
            CreateMap<Person, PersonDto>()
                .ReverseMap()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnorePath(d => d.Birth.StateProvince.Code)
                .IgnorePath(d => d.Birth.Country.Code);
            CreateMap<ReferenceStateProvince, ReferenceStateProvinceDto>()
                .ReverseMap()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnorePath(d => d.Country.Code);
        }
    }

    [ExcludeFromCodeCoverage]
    public static class MappingHelper
    {
        /// <summary>
        /// Extension to ignore a member of the main destination class
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static IMappingExpression<TSource, TDestination> IgnoreMember<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }

        /// <summary>
        /// Extension to ignore a path back to the source class in the reverse map
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static IMappingExpression<TSource, TDestination> IgnorePath<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForPath(selector, config => config.Ignore());
            return map;
        }

        /*

         * Possible room for improvement to detect properties that are private and
         * exclude them from the map.  StackOverflowException issue due to the circular
         * reference within Person and Birth, leaving to modify later
         * TODO DTS 10/2/2018
         *
        /// <summary>
        /// Extension to ignore a path back to the source class in the reverse map
        /// </summary>
        public static IMappingExpression<TSource, TDestination> IgnoreAllRecursivePropertiesWithAnInaccessibleSetter<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            if (selector.Type.GetProperties().Length > 0)
            {
                foreach (PropertyInfo propertyInfo in selector.Body.Type.GetProperties())
                {
                    IgnoreProperty(ref map, propertyInfo);
                }
            }

            return map;
        }

        public static void IgnoreProperty<TSource, TDestination>(
            ref IMappingExpression<TSource, TDestination> map,
            PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.Namespace.StartsWith("PASRI"))
            {
                if (propertyInfo.PropertyType.GetProperties().Length > 0)
                {
                    foreach (PropertyInfo childProperty in propertyInfo.PropertyType.GetProperties())
                    {
                        IgnoreProperty(ref map, childProperty);
                    }
                }
            }

            if (propertyInfo.GetSetMethod() != null && propertyInfo.GetSetMethod().IsPrivate)
            {
                var entityType = propertyInfo.DeclaringType;
                var parameter = Expression.Parameter(entityType, propertyInfo.Name);
                var property = Expression.Property(parameter, propertyInfo);
                var funcType = typeof(Func<,>).MakeGenericType(entityType, propertyInfo.PropertyType);
                var lambda = Expression.Lambda(funcType, property, parameter);

                map.GetType().GetMethod("ForPath")
                    .MakeGenericMethod(propertyInfo.PropertyType)
                    .Invoke(map, new[] {lambda});
            }
        }
        */
    }
}

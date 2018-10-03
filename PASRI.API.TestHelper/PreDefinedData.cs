using System;
using PASRI.API.Core.Domain;
// ReSharper disable CoVariantArrayConversion

namespace PASRI.API.TestHelper
{
    public static class PreDefinedData
    {
        #region Persons

        public static readonly Person[] Persons = {
            new Person { Id = 1, FirstName = "First", MiddleName = "Middle", LastName = "Person", SuffixId = 1, EffectiveDate = DateTime.Parse("10/1/2018") },
            new Person { Id = 2, FirstName = "Second", MiddleName = "Middle", LastName = "Person", SuffixId = 2, EffectiveDate = DateTime.Parse("10/1/2018") },
            new Person { Id = 3, FirstName = "Third", LastName = "Person", SuffixId = 3, EffectiveDate = DateTime.Parse("10/1/2018") },
            new Person { Id = 4, FirstName = "Fourth", LastName = "Person", EffectiveDate = DateTime.Parse("10/1/2018") }
        };

        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="Person"/> that exists in the
        /// <see cref="PreDefinedData.Persons"/> test collection
        /// </summary>
        public static int GetRandomPersonId()
        {
            return Persons[
                new Random().Next(0, Persons.Length)
            ].Id;
        }

        #endregion

        #region Births

        public static readonly Birth[] Births = {
            new Birth { Id = 1, PersonId = 1, Date = DateTime.Parse("10/1/2000"), City = "San Antonio", StateProvinceId = 1, CountryId = 1 },
            new Birth { Id = 2, PersonId = 2, Date = DateTime.Parse("10/2/2000"), City = "College Station", StateProvinceId = 1, CountryId = 1 },
            new Birth { Id = 3, PersonId = 3, Date = DateTime.Parse("10/3/2000"), City = "Vancouver", CountryId = 2 }
        };

        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="Birth"/> that exists in the
        /// <see cref="PreDefinedData.Births"/> test collection
        /// </summary>
        public static int GetRandomBirthId()
        {
            return Births[
                new Random().Next(0, Births.Length)
            ].Id;
        }

        #endregion

        #region ReferenceBloodTypes

        public static readonly ReferenceBloodType[] ReferenceBloodTypes = {
            new ReferenceBloodType { Id = 1, Code = "O", LongName = "No antigens, A and B antibodies" },
            new ReferenceBloodType { Id = 2, Code = "A", LongName = "A antigen, B antibody" },
            new ReferenceBloodType { Id = 3, Code = "B", LongName = "B antigen, A antibody" },
            new ReferenceBloodType { Id = 4, Code = "AB", LongName = "A and B antigen, no antibodies" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceBloodType"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceBloodTypes"/> test collection
        /// </summary>
        public static string GetNotExistsBloodTypeCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceBloodTypes,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceBloodType"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceBloodTypes"/> test collection
        /// </summary>
        public static int GetRandomBloodTypeId()
        {
            return ReferenceBloodTypes[
                new Random().Next(0, ReferenceBloodTypes.Length)
            ].Id;
        }

        #endregion

        #region ReferenceCountries

        public static readonly ReferenceCountry[] ReferenceCountries = {
            new ReferenceCountry { Id = 1, Code = "US", LongName = "United States of America" },
            new ReferenceCountry { Id = 2, Code = "CA", LongName = "Canada" },
            new ReferenceCountry { Id = 3, Code = "MX", LongName = "Mexico" }
        };

        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="ReferenceCountry"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceCountries"/> test collection
        /// </summary>
        public static string GetNotExistsCountryCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceCountries,
                "Code", 2);
        }

        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="ReferenceCountry"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceCountries"/> test collection
        /// </summary>
        public static int GetRandomCountryId()
        {
            return ReferenceCountries[
                new Random().Next(0, ReferenceCountries.Length)
            ].Id;
        }

        #endregion

        #region ReferenceEthnicGroupDemographics

        public static readonly ReferenceEthnicGroupDemographic[] ReferenceEthnicGroupDemographics = {
            new ReferenceEthnicGroupDemographic { Id = 1, Code = "AA", LongName = "Asian Indian" },
            new ReferenceEthnicGroupDemographic { Id = 2, Code = "AB", LongName = "Chinese" },
            new ReferenceEthnicGroupDemographic { Id = 3, Code = "AC", LongName = "Filipino" },
            new ReferenceEthnicGroupDemographic { Id = 4, Code = "BG", LongName = "Other" }
        };

        /// <summary>
        /// Helper method to retrieve a ethnic group demographic code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroupDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroupDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsEthnicGroupDemographicCode() =>
            AssertHelper.GetValueNotInArray(ReferenceEthnicGroupDemographics,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a ethnic group demographic code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroupDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroupDemographics"/> test collection
        /// </summary>
        public static int GetRandomEthnicGroupDemographicId() =>
            ReferenceEthnicGroupDemographics[
                new Random().Next(0, ReferenceEthnicGroupDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceEyeColors

        public static readonly ReferenceEyeColor[] ReferenceEyeColors = {
            new ReferenceEyeColor { Id = 1, Code = "BR", LongName = "Brown" },
            new ReferenceEyeColor { Id = 2, Code = "GR", LongName = "Green" },
            new ReferenceEyeColor { Id = 3, Code = "BL", LongName = "Blue" }
        };

        /// <summary>
        /// Helper method to retrieve a eye color code, which is the primary key
        /// of the <see cref="ReferenceEyeColor"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceEyeColors"/> test collection
        /// </summary>
        public static string GetNotExistsEyeColorCode() =>
            AssertHelper.GetValueNotInArray(ReferenceEyeColors,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a eye color code, which is the primary key
        /// of the <see cref="ReferenceEyeColor"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceEyeColors"/> test collection
        /// </summary>
        public static int GetRandomEyeColorId() =>
            ReferenceEyeColors[
                new Random().Next(0, ReferenceEyeColors.Length)
            ].Id;

        #endregion

        #region ReferenceGenderDemographics

        public static readonly ReferenceGenderDemographic[] ReferenceGenderDemographics = {
            new ReferenceGenderDemographic { Id = 1, Code = "Z", LongName = "Unknown" },
            new ReferenceGenderDemographic { Id = 2, Code = "F", LongName = "Female" },
            new ReferenceGenderDemographic { Id = 3, Code = "M", LongName = "Male" }
        };

        /// <summary>
        /// Helper method to retrieve a gender demographic code, which is the primary key
        /// of the <see cref="ReferenceGenderDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceGenderDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsGenderDemographicCode() =>
            AssertHelper.GetValueNotInArray(ReferenceGenderDemographics,
                "Code", 1);

        /// <summary>
        /// Helper method to retrieve a gender demographic code, which is the primary key
        /// of the <see cref="ReferenceGenderDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceGenderDemographics"/> test collection
        /// </summary>
        public static int GetRandomGenderDemographicId() =>
            ReferenceGenderDemographics[
                new Random().Next(0, ReferenceGenderDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceHairColors

        public static readonly ReferenceHairColor[] ReferenceHairColors = {
            new ReferenceHairColor { Id = 1, Code = "BR", LongName = "Brown" },
            new ReferenceHairColor { Id = 2, Code = "BK", LongName = "Black" },
            new ReferenceHairColor { Id = 3, Code = "BL", LongName = "Blond" }
        };

        /// <summary>
        /// Helper method to retrieve a hair color code, which is the primary key
        /// of the <see cref="ReferenceHairColor"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceHairColors"/> test collection
        /// </summary>
        public static string GetNotExistsHairColorCode() =>
            AssertHelper.GetValueNotInArray(ReferenceHairColors,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a hair color code, which is the primary key
        /// of the <see cref="ReferenceHairColor"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceHairColors"/> test collection
        /// </summary>
        public static int GetRandomHairColorId() =>
            ReferenceHairColors[
                new Random().Next(0, ReferenceHairColors.Length)
            ].Id;

        #endregion

        #region ReferenceNameSuffixes

        public static readonly ReferenceNameSuffix[] ReferenceNameSuffixes = {
            new ReferenceNameSuffix { Id = 1, Code = "Jr", LongName = "Junior" },
            new ReferenceNameSuffix { Id = 2, Code = "Sr", LongName = "Senior" },
            new ReferenceNameSuffix { Id = 3, Code = "III", LongName = "The Third" }
        };

        /// <summary>
        /// Helper method to retrieve a suffix name code, which is the primary key
        /// of the <see cref="ReferenceNameSuffix"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceNameSuffixes"/> test collection
        /// </summary>
        public static string GetNotExistsNameSuffixCode() =>
            AssertHelper.GetValueNotInArray(ReferenceNameSuffixes,
                "Code", 4);

        /// <summary>
        /// Helper method to retrieve a suffix name code, which is the primary key
        /// of the <see cref="ReferenceNameSuffix"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceNameSuffixes"/> test collection
        /// </summary>
        public static int GetRandomNameSuffixId() =>
            ReferenceNameSuffixes[
                new Random().Next(0, ReferenceNameSuffixes.Length)
            ].Id;

        #endregion

        #region ReferenceRaceDemographics

        public static readonly ReferenceRaceDemographic[] ReferenceRaceDemographics = {
            new ReferenceRaceDemographic { Id = 1, Code = "A", LongName = "American Indian/Alaskan Native" },
            new ReferenceRaceDemographic { Id = 2, Code = "B", LongName = "Asian" },
            new ReferenceRaceDemographic { Id = 3, Code = "C", LongName = "Black" },
            new ReferenceRaceDemographic { Id = 4, Code = "D", LongName = "Native Hawaiian or other Pacific Islander" },
            new ReferenceRaceDemographic { Id = 5, Code = "E", LongName = "White" },
            new ReferenceRaceDemographic { Id = 6, Code = "F", LongName = "Declined to Respond" }
        };

        /// <summary>
        /// Helper method to retrieve a race demographic code, which is the primary key
        /// of the <see cref="ReferenceRaceDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceRaceDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsRaceDemographicCode() =>
            AssertHelper.GetValueNotInArray(ReferenceRaceDemographics,
                "Code", 1);

        /// <summary>
        /// Helper method to retrieve a race demographic code, which is the primary key
        /// of the <see cref="ReferenceRaceDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceRaceDemographics"/> test collection
        /// </summary>
        public static int GetRandomRaceDemographicId() =>
            ReferenceRaceDemographics[
                new Random().Next(0, ReferenceRaceDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceReligionDemographics

        public static readonly ReferenceReligionDemographic[] ReferenceReligionDemographics = {
            new ReferenceReligionDemographic { Id = 1, Code = "AC", LongName = "Advent Christian Church" },
            new ReferenceReligionDemographic { Id = 2, Code = "AV", LongName = "Adventist Churches - Excludes Adv ent Christian Church, Jehovah's Witnesses, Native American, and Seventh Day Adventist" },
            new ReferenceReligionDemographic { Id = 3, Code = "ME", LongName = "African Methodist Episcopal Church" }
        };

        /// <summary>
        /// Helper method to retrieve a religion demographic code, which is the primary key
        /// of the <see cref="ReferenceReligionDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceReligionDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsReligionDemographicCode() =>
            AssertHelper.GetValueNotInArray(ReferenceReligionDemographics,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a religion demographic code, which is the primary key
        /// of the <see cref="ReferenceReligionDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceReligionDemographics"/> test collection
        /// </summary>
        public static int GetRandomReligionDemographicId() =>
            ReferenceReligionDemographics[
                new Random().Next(0, ReferenceReligionDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceStateProvinces

        public static readonly ReferenceStateProvince[] ReferenceStateProvinces = {
            new ReferenceStateProvince { Id = 1, Code = "TX", LongName = "Texas" },
            new ReferenceStateProvince { Id = 2, Code = "KY", LongName = "Kentucky" },
            new ReferenceStateProvince { Id = 3, Code = "DE", LongName = "Delaware" }
        };

        /// <summary>
        /// Helper method to retrieve a state code, which is the primary key
        /// of the <see cref="ReferenceStateProvince"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceStateProvinces"/> test collection
        /// </summary>
        public static string GetNotExistsStateProvinceCode() =>
            AssertHelper.GetValueNotInArray(ReferenceStateProvinces,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a state code, which is the primary key
        /// of the <see cref="ReferenceStateProvince"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceStateProvinces"/> test collection
        /// </summary>
        public static int GetRandomStateProvinceId() =>
            ReferenceStateProvinces[
                new Random().Next(0, ReferenceStateProvinces.Length)
            ].Id;

        #endregion
    }
}

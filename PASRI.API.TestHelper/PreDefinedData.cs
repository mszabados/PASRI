using System;
using PASRI.API.Core.Domain;

namespace PASRI.API.TestHelper
{
    public class PreDefinedData
    {
        #region ReferenceBloodTypes

        public static ReferenceBloodType[] ReferenceBloodTypes = new[]
        {
            new ReferenceBloodType() { Id = 1, Code = "O", Description = "No antigens, A and B antibodies" },
            new ReferenceBloodType() { Id = 2, Code = "A", Description = "A antigen, B antibody" },
            new ReferenceBloodType() { Id = 3, Code = "B", Description = "B antigen, A antibody" },
            new ReferenceBloodType() { Id = 4, Code = "AB", Description = "A and B antigen, no antibodies" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceBloodType"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceBloodTypes"/> test collection
        /// </summary>
        public static string GetNotExistsBloodTypeCode()
        {
            return AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceBloodTypes,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceBloodType"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceBloodTypes"/> test collection
        /// </summary>
        public static int GetRandomBloodTypeId()
        {
            return PreDefinedData.ReferenceBloodTypes[
                new Random().Next(0, PreDefinedData.ReferenceBloodTypes.Length)
            ].Id;
        }

        #endregion

        #region ReferenceCountries

        public static ReferenceCountry[] ReferenceCountries = new[]
        {
            new ReferenceCountry() { Id = 1, Code = "US", Description = "United States of America" },
            new ReferenceCountry() { Id = 2, Code = "CA", Description = "Canada" },
            new ReferenceCountry() { Id = 3, Code = "MX", Description = "Mexico" }
        };

        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="ReferenceCountry"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceCountries"/> test collection
        /// </summary>
        public static string GetNotExistsCountryCode()
        {
            return AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceCountries,
                "Code", 2);
        }

        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="ReferenceCountry"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceCountries"/> test collection
        /// </summary>
        public static int GetRandomCountryId()
        {
            return PreDefinedData.ReferenceCountries[
                new Random().Next(0, PreDefinedData.ReferenceCountries.Length)
            ].Id;
        }

        #endregion

        #region ReferenceEthnicGroupDemographics

        public static ReferenceEthnicGroupDemographic[] ReferenceEthnicGroupDemographics = new[]
        {
            new ReferenceEthnicGroupDemographic() { Id = 1, Code = "AA", Description = "Asian Indian" },
            new ReferenceEthnicGroupDemographic() { Id = 2, Code = "AB", Description = "Chinese" },
            new ReferenceEthnicGroupDemographic() { Id = 3, Code = "AC", Description = "Filipino" },
            new ReferenceEthnicGroupDemographic() { Id = 4, Code = "BG", Description = "Other" }
        };

        /// <summary>
        /// Helper method to retrieve a ethnic group demographic code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroupDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroupDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsEthnicGroupDemographicCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceEthnicGroupDemographics,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a ethnic group demographic code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroupDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroupDemographics"/> test collection
        /// </summary>
        public static int GetRandomEthnicGroupDemographicId() =>
            PreDefinedData.ReferenceEthnicGroupDemographics[
                new Random().Next(0, PreDefinedData.ReferenceEthnicGroupDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceEyeColors

        public static ReferenceEyeColor[] ReferenceEyeColors = new[]
        {
            new ReferenceEyeColor() { Id = 1, Code = "BR", Description = "Brown" },
            new ReferenceEyeColor() { Id = 2, Code = "GR", Description = "Green" },
            new ReferenceEyeColor() { Id = 3, Code = "BL", Description = "Blue" }
        };

        /// <summary>
        /// Helper method to retrieve a eye color code, which is the primary key
        /// of the <see cref="ReferenceEyeColor"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceEyeColors"/> test collection
        /// </summary>
        public static string GetNotExistsEyeColorCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceEyeColors,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a eye color code, which is the primary key
        /// of the <see cref="ReferenceEyeColor"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceEyeColors"/> test collection
        /// </summary>
        public static int GetRandomEyeColorId() =>
            PreDefinedData.ReferenceEyeColors[
                new Random().Next(0, PreDefinedData.ReferenceEyeColors.Length)
            ].Id;

        #endregion

        #region ReferenceGenderDemographics

        public static ReferenceGenderDemographic[] ReferenceGenderDemographics = new[]
        {
            new ReferenceGenderDemographic() { Id = 1, Code = "Z", Description = "Unknown" },
            new ReferenceGenderDemographic() { Id = 2, Code = "F", Description = "Female" },
            new ReferenceGenderDemographic() { Id = 3, Code = "M", Description = "Male" }
        };

        /// <summary>
        /// Helper method to retrieve a gender demographic code, which is the primary key
        /// of the <see cref="ReferenceGenderDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceGenderDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsGenderDemographicCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceGenderDemographics,
                "Code", 1);

        /// <summary>
        /// Helper method to retrieve a gender demographic code, which is the primary key
        /// of the <see cref="ReferenceGenderDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceGenderDemographics"/> test collection
        /// </summary>
        public static int GetRandomGenderDemographicId() =>
            PreDefinedData.ReferenceGenderDemographics[
                new Random().Next(0, PreDefinedData.ReferenceGenderDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceHairColors

        public static ReferenceHairColor[] ReferenceHairColors = new[]
        {
            new ReferenceHairColor() { Id = 1, Code = "BR", Description = "Brown" },
            new ReferenceHairColor() { Id = 2, Code = "BK", Description = "Black" },
            new ReferenceHairColor() { Id = 3, Code = "BL", Description = "Blond" }
        };

        /// <summary>
        /// Helper method to retrieve a hair color code, which is the primary key
        /// of the <see cref="ReferenceHairColor"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceHairColors"/> test collection
        /// </summary>
        public static string GetNotExistsHairColorCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceHairColors,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a hair color code, which is the primary key
        /// of the <see cref="ReferenceHairColor"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceHairColors"/> test collection
        /// </summary>
        public static int GetRandomHairColorId() =>
            PreDefinedData.ReferenceHairColors[
                new Random().Next(0, PreDefinedData.ReferenceHairColors.Length)
            ].Id;

        #endregion

        #region ReferenceRaceDemographics

        public static ReferenceRaceDemographic[] ReferenceRaceDemographics = new[]
        {
            new ReferenceRaceDemographic() { Id = 1, Code = "A", Description = "American Indian/Alaskan Native" },
            new ReferenceRaceDemographic() { Id = 2, Code = "B", Description = "Asian" },
            new ReferenceRaceDemographic() { Id = 3, Code = "C", Description = "Black" },
            new ReferenceRaceDemographic() { Id = 4, Code = "D", Description = "Native Hawaiian or other Pacific Islander" },
            new ReferenceRaceDemographic() { Id = 5, Code = "E", Description = "White" },
            new ReferenceRaceDemographic() { Id = 6, Code = "F", Description = "Declined to Respond" }
        };

        /// <summary>
        /// Helper method to retrieve a race demographic code, which is the primary key
        /// of the <see cref="ReferenceRaceDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceRaceDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsRaceDemographicCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceRaceDemographics,
                "Code", 1);

        /// <summary>
        /// Helper method to retrieve a race demographic code, which is the primary key
        /// of the <see cref="ReferenceRaceDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceRaceDemographics"/> test collection
        /// </summary>
        public static int GetRandomRaceDemographicId() =>
            PreDefinedData.ReferenceRaceDemographics[
                new Random().Next(0, PreDefinedData.ReferenceRaceDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceReligionDemographics

        public static ReferenceReligionDemographic[] ReferenceReligionDemographics = new[]
        {
            new ReferenceReligionDemographic() { Id = 1, Code = "AC", Description = "Advent Christian Church" },
            new ReferenceReligionDemographic() { Id = 2, Code = "AV", Description = "Adventist Churches - Excludes Adv ent Christian Church, Jehovah's Witnesses, Native American, and Seventh Day Adventist" },
            new ReferenceReligionDemographic() { Id = 3, Code = "ME", Description = "African Methodist Episcopal Church" }
        };

        /// <summary>
        /// Helper method to retrieve a religion demographic code, which is the primary key
        /// of the <see cref="ReferenceReligionDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceReligionDemographics"/> test collection
        /// </summary>
        public static string GetNotExistsReligionDemographicCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceReligionDemographics,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a religion demographic code, which is the primary key
        /// of the <see cref="ReferenceReligionDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceReligionDemographics"/> test collection
        /// </summary>
        public static int GetRandomReligionDemographicId() =>
            PreDefinedData.ReferenceReligionDemographics[
                new Random().Next(0, PreDefinedData.ReferenceReligionDemographics.Length)
            ].Id;

        #endregion

        #region ReferenceStateProvinces

        public static ReferenceStateProvince[] ReferenceStateProvinces = new[]
        {
            new ReferenceStateProvince() { Id = 1, Code = "TX", Description = "Texas" },
            new ReferenceStateProvince() { Id = 2, Code = "KY", Description = "Kentucky" },
            new ReferenceStateProvince() { Id = 3, Code = "DE", Description = "Delaware" }
        };

        /// <summary>
        /// Helper method to retrieve a state code, which is the primary key
        /// of the <see cref="ReferenceStateProvince"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceStateProvinces"/> test collection
        /// </summary>
        public static string GetNotExistsStateProvinceCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceStateProvinces,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a state code, which is the primary key
        /// of the <see cref="ReferenceStateProvince"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceStateProvinces"/> test collection
        /// </summary>
        public static int GetRandomStateProvinceId() =>
            PreDefinedData.ReferenceStateProvinces[
                new Random().Next(0, PreDefinedData.ReferenceStateProvinces.Length)
            ].Id;

        #endregion

        #region ReferenceNameSuffixes

        public static ReferenceNameSuffix[] ReferenceNameSuffixes = new[]
        {
            new ReferenceNameSuffix() { Id = 1, Code = "Jr", Description = "Junior" },
            new ReferenceNameSuffix() { Id = 2, Code = "Sr", Description = "Senior" },
            new ReferenceNameSuffix() { Id = 3, Code = "III", Description = "The Third" }
        };

        /// <summary>
        /// Helper method to retrieve a suffix name code, which is the primary key
        /// of the <see cref="ReferenceNameSuffix"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceNameSuffixes"/> test collection
        /// </summary>
        public static string GetNotExistsNameSuffixCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceNameSuffixes,
                "Code", 4);

        /// <summary>
        /// Helper method to retrieve a suffix name code, which is the primary key
        /// of the <see cref="ReferenceNameSuffix"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceNameSuffixes"/> test collection
        /// </summary>
        public static int GetRandomNameSuffixId() =>
            PreDefinedData.ReferenceNameSuffixes[
                new Random().Next(0, PreDefinedData.ReferenceNameSuffixes.Length)
            ].Id;

        #endregion
    }
}

using System;
using PASRI.API.Core.Domain;

namespace PASRI.API.TestHelper
{
    public class PreDefinedData
    {
        #region ReferenceBloodTypes

        public static ReferenceBloodType[] ReferenceBloodTypes = new[]
        {
            new ReferenceBloodType() { Code = "O", DisplayText = "No antigens, A and B antibodies", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceBloodType() { Code = "A", DisplayText = "A antigen, B antibody" },
            new ReferenceBloodType() { Code = "B", DisplayText = "B antigen, A antibody" },
            new ReferenceBloodType() { Code = "AB", DisplayText = "A and B antigen, no antibodies" }
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
        public static string GetRandomBloodTypeCode()
        {
            return PreDefinedData.ReferenceBloodTypes[
                new Random().Next(0, PreDefinedData.ReferenceBloodTypes.Length)
            ].Code;
        }

        #endregion

        #region ReferenceCountries

        public static ReferenceCountry[] ReferenceCountries = new[]
        {
            new ReferenceCountry() { Code = "US", DisplayText = "United States of America", StartDate = DateTime.Parse("7/4/1776 9:30 AM"), EndDate = DateTime.MaxValue },
            new ReferenceCountry() { Code = "CA", DisplayText = "Canada" },
            new ReferenceCountry() { Code = "MX", DisplayText = "Mexico" }
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
        public static string GetRandomCountryCode()
        {
            return PreDefinedData.ReferenceCountries[
                new Random().Next(0, PreDefinedData.ReferenceCountries.Length)
            ].Code;
        }

        #endregion

        #region ReferenceEthnicGroupDemographics

        public static ReferenceEthnicGroupDemographic[] ReferenceEthnicGroupDemographics = new[]
        {
            new ReferenceEthnicGroupDemographic() { Code = "AA", DisplayText = "Asian Indian", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceEthnicGroupDemographic() { Code = "AB", DisplayText = "Chinese" },
            new ReferenceEthnicGroupDemographic() { Code = "AC", DisplayText = "Filipino" },
            new ReferenceEthnicGroupDemographic() { Code = "BG", DisplayText = "Other" }
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
        public static string GetRandomEthnicGroupDemographicCode() =>
            PreDefinedData.ReferenceEthnicGroupDemographics[
                new Random().Next(0, PreDefinedData.ReferenceEthnicGroupDemographics.Length)
            ].Code;

        #endregion

        #region ReferenceEyeColors

        public static ReferenceEyeColor[] ReferenceEyeColors = new[]
        {
            new ReferenceEyeColor() { Code = "BR", DisplayText = "Brown", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceEyeColor() { Code = "GR", DisplayText = "Green" },
            new ReferenceEyeColor() { Code = "BL", DisplayText = "Blue" }
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
        public static string GetRandomEyeColorCode() =>
            PreDefinedData.ReferenceEyeColors[
                new Random().Next(0, PreDefinedData.ReferenceEyeColors.Length)
            ].Code;

        #endregion

        #region ReferenceGenderDemographics

        public static ReferenceGenderDemographic[] ReferenceGenderDemographics = new[]
        {
            new ReferenceGenderDemographic() { Code = "Z", DisplayText = "Unknown", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceGenderDemographic() { Code = "F", DisplayText = "Female" },
            new ReferenceGenderDemographic() { Code = "M", DisplayText = "Male" }
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
        public static string GetRandomGenderDemographicCode() =>
            PreDefinedData.ReferenceGenderDemographics[
                new Random().Next(0, PreDefinedData.ReferenceGenderDemographics.Length)
            ].Code;

        #endregion

        #region ReferenceHairColors

        public static ReferenceHairColor[] ReferenceHairColors = new[]
        {
            new ReferenceHairColor() { Code = "BR", DisplayText = "Brown", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceHairColor() { Code = "BK", DisplayText = "Black" },
            new ReferenceHairColor() { Code = "BL", DisplayText = "Blond" }
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
        public static string GetRandomHairColorCode() =>
            PreDefinedData.ReferenceHairColors[
                new Random().Next(0, PreDefinedData.ReferenceHairColors.Length)
            ].Code;

        #endregion

        #region ReferenceRaceDemographics

        public static ReferenceRaceDemographic[] ReferenceRaceDemographics = new[]
        {
            new ReferenceRaceDemographic() { Code = "A", DisplayText = "American Indian/Alaskan Native", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceRaceDemographic() { Code = "B", DisplayText = "Asian" },
            new ReferenceRaceDemographic() { Code = "C", DisplayText = "Black" },
            new ReferenceRaceDemographic() { Code = "D", DisplayText = "Native Hawaiian or other Pacific Islander" },
            new ReferenceRaceDemographic() { Code = "E", DisplayText = "White" },
            new ReferenceRaceDemographic() { Code = "F", DisplayText = "Declined to Respond" }
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
        public static string GetRandomRaceDemographicCode() =>
            PreDefinedData.ReferenceRaceDemographics[
                new Random().Next(0, PreDefinedData.ReferenceRaceDemographics.Length)
            ].Code;

        #endregion

        #region ReferenceReligionDemographics

        public static ReferenceReligionDemographic[] ReferenceReligionDemographics = new[]
        {
            new ReferenceReligionDemographic() { Code = "AC", DisplayText = "Advent Christian Church", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceReligionDemographic() { Code = "AV", DisplayText = "Adventist Churches - Excludes Adv ent Christian Church, Jehovah's Witnesses, Native American, and Seventh Day Adventist" },
            new ReferenceReligionDemographic() { Code = "ME", DisplayText = "African Methodist Episcopal Church" }
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
        public static string GetRandomReligionDemographicCode() =>
            PreDefinedData.ReferenceReligionDemographics[
                new Random().Next(0, PreDefinedData.ReferenceReligionDemographics.Length)
            ].Code;

        #endregion

        #region ReferenceStates

        public static ReferenceState[] ReferenceStates = new[]
        {
            new ReferenceState() { Code = "TX", DisplayText = "Texas", StartDate = DateTime.Parse("12/29/1845"), EndDate = DateTime.MaxValue },
            new ReferenceState() { Code = "KY", DisplayText = "Kentucky" },
            new ReferenceState() { Code = "DE", DisplayText = "Delaware" }
        };

        /// <summary>
        /// Helper method to retrieve a state code, which is the primary key
        /// of the <see cref="ReferenceState"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceStates"/> test collection
        /// </summary>
        public static string GetNotExistsStateCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceStates,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a state code, which is the primary key
        /// of the <see cref="ReferenceState"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceStates"/> test collection
        /// </summary>
        public static string GetRandomStateCode() =>
            PreDefinedData.ReferenceStates[
                new Random().Next(0, PreDefinedData.ReferenceStates.Length)
            ].Code;

        #endregion

        #region ReferenceSuffixNames

        public static ReferenceSuffixName[] ReferenceSuffixNames = new[]
        {
            new ReferenceSuffixName() { Code = "Jr", DisplayText = "Junior", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceSuffixName() { Code = "Sr", DisplayText = "Senior" },
            new ReferenceSuffixName() { Code = "III", DisplayText = "The Third" }
        };

        /// <summary>
        /// Helper method to retrieve a suffix name code, which is the primary key
        /// of the <see cref="ReferenceSuffixName"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceSuffixNames"/> test collection
        /// </summary>
        public static string GetNotExistsSuffixNameCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceSuffixNames,
                "Code", 4);

        /// <summary>
        /// Helper method to retrieve a suffix name code, which is the primary key
        /// of the <see cref="ReferenceSuffixName"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceSuffixNames"/> test collection
        /// </summary>
        public static string GetRandomSuffixNameCode() =>
            PreDefinedData.ReferenceSuffixNames[
                new Random().Next(0, PreDefinedData.ReferenceSuffixNames.Length)
            ].Code;

        #endregion
    }
}

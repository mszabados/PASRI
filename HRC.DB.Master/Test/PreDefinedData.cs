using HRC.DB.Master.Core.Domain;
using System;
using HRC.Helper.Test;

// ReSharper disable CoVariantArrayConversion

namespace HRC.DB.Master.Test
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

        #region ReferenceAccessionSources

        public static readonly ReferenceAccessionSource[] ReferenceAccessionSources = {
            new ReferenceAccessionSource { Id = 1, Code = "EA", LongName = "Enlisted - Induction" },
            new ReferenceAccessionSource { Id = 2, Code = "EZ", LongName = "Enlisted - Unknown" },
            new ReferenceAccessionSource { Id = 3, Code = "WZ", LongName = "Warrant Officer - Unknown" },
            new ReferenceAccessionSource { Id = 4, Code = "OZ", LongName = "Commissioned Officer - Unknown" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceAccessionSource"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceAccessionSources"/> test collection
        /// </summary>
        public static string GetNotExistsAccessionSourceCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceAccessionSources,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceAccessionSource"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceAccessionSources"/> test collection
        /// </summary>
        public static int GetRandomAccessionSourceId()
        {
            return ReferenceAccessionSources[
                new Random().Next(0, ReferenceAccessionSources.Length)
            ].Id;
        }

        #endregion

        #region ReferenceBasesForUsCitizenships

        public static readonly ReferenceBasisForUsCitizenship[] ReferenceBasesForUsCitizenship = {
            new ReferenceBasisForUsCitizenship { Id = 1, Code = "A", LongName = "Native born. A person born in one of the 50 United States, Puerto Rico, Guam, American Samoa, Northern Marina Islands; U.S. Virgin Islands; or Panama Canal Zone (if the father or mother (or both) was or is a citizen of the United States)." },
            new ReferenceBasisForUsCitizenship { Id = 2, Code = "B", LongName = "Naturalized. A person born outside of the United States who has completed naturalization procedures and has been given U.S. citizenship by duly constituted authority." },
            new ReferenceBasisForUsCitizenship { Id = 3, Code = "C", LongName = "Derivative birth. A person born outside the United States who acquires U.S. citizenship at birth because one or both of his or her parents are U.S. citizens at the time of the person’s birth." },
            new ReferenceBasisForUsCitizenship { Id = 4, Code = "D", LongName = "Derivative naturalization. A person who acquires U.S. citizenship after birth through naturalization of one or both parents." }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceBasisForUsCitizenship"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceBasesForUsCitizenship"/> test collection
        /// </summary>
        public static string GetNotExistsBasisForUsCitizenshipCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceBasesForUsCitizenship,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceBasisForUsCitizenship"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceBasesForUsCitizenship"/> test collection
        /// </summary>
        public static int GetRandomBasisForUsCitizenshipId()
        {
            return ReferenceBasesForUsCitizenship[
                new Random().Next(0, ReferenceBasesForUsCitizenship.Length)
            ].Id;
        }

        #endregion

        #region ReferenceBloodTypes

        public static readonly ReferenceBloodType[] ReferenceBloodTypes = {
            new ReferenceBloodType { Id = 1, Code = "A", LongName = "A Positive" },
            new ReferenceBloodType { Id = 2, Code = "B", LongName = "A Negative" },
            new ReferenceBloodType { Id = 3, Code = "E", LongName = "O Positive" },
            new ReferenceBloodType { Id = 4, Code = "F", LongName = "O Negative" }
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

        #region ReferenceEthnicGroups

        public static readonly ReferenceEthnicGroup[] ReferenceEthnicGroups = {
            new ReferenceEthnicGroup { Id = 1, Code = "AA", LongName = "Asian Indian" },
            new ReferenceEthnicGroup { Id = 2, Code = "AB", LongName = "Chinese" },
            new ReferenceEthnicGroup { Id = 3, Code = "AC", LongName = "Filipino" },
            new ReferenceEthnicGroup { Id = 4, Code = "BG", LongName = "Other" }
        };

        /// <summary>
        /// Helper method to retrieve a ethnic group code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroup"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroups"/> test collection
        /// </summary>
        public static string GetNotExistsEthnicGroupCode() =>
            AssertHelper.GetValueNotInArray(ReferenceEthnicGroups,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a ethnic group code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroup"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroups"/> test collection
        /// </summary>
        public static int GetRandomEthnicGroupId() =>
            ReferenceEthnicGroups[
                new Random().Next(0, ReferenceEthnicGroups.Length)
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

        #region ReferenceGenders

        public static readonly ReferenceGender[] ReferenceGenders = {
            new ReferenceGender { Id = 1, Code = "Z", LongName = "Unknown" },
            new ReferenceGender { Id = 2, Code = "F", LongName = "Female" },
            new ReferenceGender { Id = 3, Code = "M", LongName = "Male" }
        };

        /// <summary>
        /// Helper method to retrieve a gender code, which is the primary key
        /// of the <see cref="ReferenceGender"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceGenders"/> test collection
        /// </summary>
        public static string GetNotExistsGenderCode() =>
            AssertHelper.GetValueNotInArray(ReferenceGenders,
                "Code", 1);

        /// <summary>
        /// Helper method to retrieve a gender code, which is the primary key
        /// of the <see cref="ReferenceGender"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceGenders"/> test collection
        /// </summary>
        public static int GetRandomGenderId() =>
            ReferenceGenders[
                new Random().Next(0, ReferenceGenders.Length)
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

        #region ReferenceMarriages

        public static readonly ReferenceMarriage[] ReferenceMarriages = {
            new ReferenceMarriage { Id = 1, Code = "M", LongName = "Married" },
            new ReferenceMarriage { Id = 2, Code = "N", LongName = "Never married" },
            new ReferenceMarriage { Id = 3, Code = "D", LongName = "Divorced" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceMarriage"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceMarriages"/> test collection
        /// </summary>
        public static string GetNotExistsMarriageCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceMarriages,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceMarriage"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceMarriages"/> test collection
        /// </summary>
        public static int GetRandomMarriageId()
        {
            return ReferenceMarriages[
                new Random().Next(0, ReferenceMarriages.Length)
            ].Id;
        }

        #endregion

        #region ReferenceMilSvcCitizenshipQuals

        public static readonly ReferenceMilSvcCitizenshipQual[] ReferenceMilSvcCitizenshipQuals = {
            new ReferenceMilSvcCitizenshipQual { Id = 1, Code = "A", LongName = "US Citizen via being born in the United States of America" },
            new ReferenceMilSvcCitizenshipQual { Id = 2, Code = "B", LongName = "US Citizen through naturalization" },
            new ReferenceMilSvcCitizenshipQual { Id = 3, Code = "H", LongName = "Nonimmigrant alien" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceMilSvcCitizenshipQual"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceMilSvcCitizenshipQuals"/> test collection
        /// </summary>
        public static string GetNotExistsMilSvcCitizenshipQualCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceMilSvcCitizenshipQuals,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceMilSvcCitizenshipQual"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceMilSvcCitizenshipQuals"/> test collection
        /// </summary>
        public static int GetRandomMilSvcCitizenshipQualId()
        {
            return ReferenceMilSvcCitizenshipQuals[
                new Random().Next(0, ReferenceMilSvcCitizenshipQuals.Length)
            ].Id;
        }

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

        #region ReferencePayPlans

        public static readonly ReferencePayPlan[] ReferencePayPlans = {
            new ReferencePayPlan { Id = 1, PersonnelClassId = 1, Code = "ME01", LongName = "Level 01" },
            new ReferencePayPlan { Id = 2, PersonnelClassId = 1, Code = "ME02", LongName = "Level 02" },
            new ReferencePayPlan { Id = 3, PersonnelClassId = 2, Code = "MO01", LongName = "Level 01" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferencePayPlan"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferencePayPlans"/> test collection
        /// </summary>
        public static string GetNotExistsPayPlanCode()
        {
            return AssertHelper.GetValueNotInArray(ReferencePayPlans,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferencePayPlan"/> that exists in the
        /// <see cref="PreDefinedData.ReferencePayPlans"/> test collection
        /// </summary>
        public static int GetRandomPayPlanId()
        {
            return ReferencePayPlans[
                new Random().Next(0, ReferencePayPlans.Length)
            ].Id;
        }

        #endregion

        #region ReferencePersonnelClasses

        public static readonly ReferencePersonnelClass[] ReferencePersonnelClasses = {
            new ReferencePersonnelClass { Id = 1, Code = "ME", LongName = "Enilsted (includes Officer Candidate School students)" },
            new ReferencePersonnelClass { Id = 2, Code = "MO", LongName = "Commissioned Officer" },
            new ReferencePersonnelClass { Id = 3, Code = "ZZ", LongName = "Unknown" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferencePersonnelClass"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferencePersonnelClasses"/> test collection
        /// </summary>
        public static string GetNotExistsPersonnelClassCode()
        {
            return AssertHelper.GetValueNotInArray(ReferencePersonnelClasses,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferencePersonnelClass"/> that exists in the
        /// <see cref="PreDefinedData.ReferencePersonnelClasses"/> test collection
        /// </summary>
        public static int GetRandomPersonnelClassId()
        {
            return ReferencePersonnelClasses[
                new Random().Next(0, ReferencePersonnelClasses.Length)
            ].Id;
        }

        #endregion

        #region ReferenceRaces

        public static readonly ReferenceRace[] ReferenceRaces = {
            new ReferenceRace { Id = 1, Code = "A", LongName = "American Indian/Alaskan Native" },
            new ReferenceRace { Id = 2, Code = "B", LongName = "Asian" },
            new ReferenceRace { Id = 3, Code = "C", LongName = "Black" },
            new ReferenceRace { Id = 4, Code = "D", LongName = "Native Hawaiian or other Pacific Islander" },
            new ReferenceRace { Id = 5, Code = "E", LongName = "White" },
            new ReferenceRace { Id = 6, Code = "F", LongName = "Declined to Respond" }
        };

        /// <summary>
        /// Helper method to retrieve a race code, which is the primary key
        /// of the <see cref="ReferenceRace"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceRaces"/> test collection
        /// </summary>
        public static string GetNotExistsRaceCode() =>
            AssertHelper.GetValueNotInArray(ReferenceRaces,
                "Code", 1);

        /// <summary>
        /// Helper method to retrieve a race code, which is the primary key
        /// of the <see cref="ReferenceRace"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceRaces"/> test collection
        /// </summary>
        public static int GetRandomRaceId() =>
            ReferenceRaces[
                new Random().Next(0, ReferenceRaces.Length)
            ].Id;

        #endregion

        #region ReferenceRanks

        public static readonly ReferenceRank[] ReferenceRanks = {
            new ReferenceRank { Id = 1, Code = "CPL", LongName = "Corporal" },
            new ReferenceRank { Id = 2, Code = "CPT", LongName = "Captain" },
            new ReferenceRank { Id = 3, Code = "COL", LongName = "Colonel" },
            new ReferenceRank { Id = 4, Code = "CSR", LongName = "Cadet Senior Advanced ROTC" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceRank"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceRanks"/> test collection
        /// </summary>
        public static string GetNotExistsRankCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceRanks,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceRank"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceRanks"/> test collection
        /// </summary>
        public static int GetRandomRankId()
        {
            return ReferenceRanks[
                new Random().Next(0, ReferenceRanks.Length)
            ].Id;
        }

        #endregion

        #region ReferenceReligions

        public static readonly ReferenceReligion[] ReferenceReligions = {
            new ReferenceReligion { Id = 1, Code = "AC", LongName = "Advent Christian Church" },
            new ReferenceReligion { Id = 2, Code = "AV", LongName = "Adventist Churches - Excludes Adv ent Christian Church, Jehovah's Witnesses, Native American, and Seventh Day Adventist" },
            new ReferenceReligion { Id = 3, Code = "ME", LongName = "African Methodist Episcopal Church" }
        };

        /// <summary>
        /// Helper method to retrieve a religion code, which is the primary key
        /// of the <see cref="ReferenceReligion"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceReligions"/> test collection
        /// </summary>
        public static string GetNotExistsReligionCode() =>
            AssertHelper.GetValueNotInArray(ReferenceReligions,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a religion code, which is the primary key
        /// of the <see cref="ReferenceReligion"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceReligions"/> test collection
        /// </summary>
        public static int GetRandomReligionId() =>
            ReferenceReligions[
                new Random().Next(0, ReferenceReligions.Length)
            ].Id;

        #endregion

        #region ReferenceServiceBranches

        public static readonly ReferenceServiceBranch[] ReferenceServiceBranches = {
            new ReferenceServiceBranch { Id = 1, Code = "A", LongName = "Army" },
            new ReferenceServiceBranch { Id = 2, Code = "N", LongName = "Navy" },
            new ReferenceServiceBranch { Id = 3, Code = "F", LongName = "Air Force" },
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceServiceBranch"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceServiceBranches"/> test collection
        /// </summary>
        public static string GetNotExistsServiceBranchCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceServiceBranches,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceServiceBranch"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceServiceBranches"/> test collection
        /// </summary>
        public static int GetRandomServiceBranchId()
        {
            return ReferenceServiceBranches[
                new Random().Next(0, ReferenceServiceBranches.Length)
            ].Id;
        }

        #endregion

        #region ReferenceServiceBranchComponents

        public static readonly ReferenceServiceBranchComponent[] ReferenceServiceBranchComponents = {
            new ReferenceServiceBranchComponent { Id = 1, ServiceBranchId = 1, Code = "R", LongName = "Regular" },
            new ReferenceServiceBranchComponent { Id = 2, ServiceBranchId = 1, Code = "G", LongName = "Guard" },
            new ReferenceServiceBranchComponent { Id = 3, ServiceBranchId = 1, Code = "V", LongName = "Reserve" },
            new ReferenceServiceBranchComponent { Id = 4, ServiceBranchId = 1, Code = "Z", LongName = "Unknown, Commissioned Corps" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceServiceBranchComponent"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceServiceBranchComponents"/> test collection
        /// </summary>
        public static string GetNotExistsServiceBranchComponentCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceServiceBranchComponents,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceServiceBranchComponent"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceServiceBranchComponents"/> test collection
        /// </summary>
        public static int GetRandomServiceBranchComponentId()
        {
            return ReferenceServiceBranchComponents[
                new Random().Next(0, ReferenceServiceBranchComponents.Length)
            ].Id;
        }

        #endregion

        #region ReferenceSsnVerifications

        public static readonly ReferenceSsnVerification[] ReferenceSsnVerifications = {
            new ReferenceSsnVerification { Id = 1, Code = "0", LongName = "Verified SSN" },
            new ReferenceSsnVerification { Id = 2, Code = "2", LongName = "Name and DOB match; gender code does not match" },
            new ReferenceSsnVerification { Id = 3, Code = "6", LongName = "SSN did not verify; other reason" }
        };

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceSsnVerification"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceSsnVerifications"/> test collection
        /// </summary>
        public static string GetNotExistsSsnVerificationCode()
        {
            return AssertHelper.GetValueNotInArray(ReferenceSsnVerifications,
                "Code", 1);
        }

        /// <summary>
        /// Helper method to retrieve a blood type code, which is the primary key
        /// of the <see cref="ReferenceSsnVerification"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceSsnVerifications"/> test collection
        /// </summary>
        public static int GetRandomSsnVerificationId()
        {
            return ReferenceSsnVerifications[
                new Random().Next(0, ReferenceSsnVerifications.Length)
            ].Id;
        }

        #endregion

        #region ReferenceStateProvinces

        public static readonly ReferenceStateProvince[] ReferenceStateProvinces = {
            new ReferenceStateProvince { Id = 1, CountryId = 1, Code = "TX", LongName = "Texas" },
            new ReferenceStateProvince { Id = 2, CountryId = 1, Code = "KY", LongName = "Kentucky" },
            new ReferenceStateProvince { Id = 3, CountryId = 1, Code = "DE", LongName = "Delaware" },
            new ReferenceStateProvince { Id = 4, CountryId = 2, Code = "AB", LongName = "Alberta" },
            new ReferenceStateProvince { Id = 5, CountryId = 2, Code = "NB", LongName = "New Brunswick" },
            new ReferenceStateProvince { Id = 6, CountryId = 2, Code = "YT", LongName = "Yukon Territory" },
            new ReferenceStateProvince { Id = 7, CountryId = 3, Code = "DG", LongName = "Durango" },
            new ReferenceStateProvince { Id = 8, CountryId = 3, Code = "OA", LongName = "Oaxaca" },
            new ReferenceStateProvince { Id = 9, CountryId = 3, Code = "CU", LongName = "Coahuila" }
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

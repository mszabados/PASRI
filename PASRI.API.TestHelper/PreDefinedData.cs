using System;
using PASRI.API.Core.Domain;

namespace PASRI.API.TestHelper
{
    public class PreDefinedData
    {
        public static ReferenceBloodType[] ReferenceBloodTypes = new[]
        {
            new ReferenceBloodType() { Code = "O", DisplayText = "No antigens, A and B antibodies", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceBloodType() { Code = "A", DisplayText = "A antigen, B antibody" },
            new ReferenceBloodType() { Code = "B", DisplayText = "B antigen, A antibody" },
            new ReferenceBloodType() { Code = "AB", DisplayText = "A and B antigen, no antibodies" }
        };

        public static ReferenceCountry[] ReferenceCountries = new[]
        {
            new ReferenceCountry() { Code = "US", DisplayText = "United States of America", StartDate = DateTime.Parse("7/4/1776 9:30 AM"), EndDate = DateTime.MaxValue },
            new ReferenceCountry() { Code = "CA", DisplayText = "Canada" },
            new ReferenceCountry() { Code = "MX", DisplayText = "Mexico" }
        };

        public static ReferenceEthnicGroupDemographic[] ReferenceEthnicGroupDemographics = new[]
        {
            new ReferenceEthnicGroupDemographic() { Code = "AA", DisplayText = "Asian Indian", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceEthnicGroupDemographic() { Code = "AB", DisplayText = "Chinese" },
            new ReferenceEthnicGroupDemographic() { Code = "AC", DisplayText = "Filipino" },
            new ReferenceEthnicGroupDemographic() { Code = "BG", DisplayText = "Other" }
        };

        public static ReferenceEyeColor[] ReferenceEyeColors = new[]
        {
            new ReferenceEyeColor() { Code = "BR", DisplayText = "Brown", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceEyeColor() { Code = "GR", DisplayText = "Green" },
            new ReferenceEyeColor() { Code = "BL", DisplayText = "Blue" }
        };

        public static ReferenceGenderDemographic[] ReferenceGenderDemographics = new[]
        {
            new ReferenceGenderDemographic() { Code = "Z", DisplayText = "Unknown", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceGenderDemographic() { Code = "F", DisplayText = "Female" },
            new ReferenceGenderDemographic() { Code = "M", DisplayText = "Male" }
        };

        public static ReferenceHairColor[] ReferenceHairColors = new[]
        {
            new ReferenceHairColor() { Code = "BR", DisplayText = "Brown", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceHairColor() { Code = "BK", DisplayText = "Black" },
            new ReferenceHairColor() { Code = "BL", DisplayText = "Blond" }
        };

        public static ReferenceRaceDemographic[] ReferenceRaceDemographics = new[]
        {
            new ReferenceRaceDemographic() { Code = "A", DisplayText = "American Indian/Alaskan Native", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceRaceDemographic() { Code = "B", DisplayText = "Asian" },
            new ReferenceRaceDemographic() { Code = "C", DisplayText = "Black" },
            new ReferenceRaceDemographic() { Code = "D", DisplayText = "Native Hawaiian or other Pacific Islander" },
            new ReferenceRaceDemographic() { Code = "E", DisplayText = "White" },
            new ReferenceRaceDemographic() { Code = "F", DisplayText = "Declined to Respond" }
        };

        public static ReferenceReligionDemographic[] ReferenceReligionDemographics = new[]
        {
            new ReferenceReligionDemographic() { Code = "AC", DisplayText = "Advent Christian Church", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceReligionDemographic() { Code = "AV", DisplayText = "Adventist Churches - Excludes Adv ent Christian Church, Jehovah's Witnesses, Native American, and Seventh Day Adventist" },
            new ReferenceReligionDemographic() { Code = "ME", DisplayText = "African Methodist Episcopal Church" }
        };

        public static ReferenceState[] ReferenceStates = new[]
        {
            new ReferenceState() { Code = "TX", DisplayText = "Texas", StartDate = DateTime.Parse("12/29/1845"), EndDate = DateTime.MaxValue },
            new ReferenceState() { Code = "KY", DisplayText = "Kentucky" },
            new ReferenceState() { Code = "DE", DisplayText = "Delaware" }
        };

        public static ReferenceSuffixName[] ReferenceSuffixNames = new[]
        {
            new ReferenceSuffixName() { Code = "Jr", DisplayText = "Junior", StartDate = DateTime.Parse("1/1/1753"), EndDate = DateTime.MaxValue },
            new ReferenceSuffixName() { Code = "Sr", DisplayText = "Senior" },
            new ReferenceSuffixName() { Code = "III", DisplayText = "The Third" }
        };
    }
}

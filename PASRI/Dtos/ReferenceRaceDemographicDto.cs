using System;
using System.ComponentModel.DataAnnotations;
using PASRI.API.Persistence.EntityConfigurations;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PASRI.API.Dtos
{
    public class ReferenceRaceDemographicDto : IComparable<ReferenceRaceDemographicDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(ReferenceRaceDemographicConfiguration.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ReferenceRaceDemographicConfiguration.LongNameLength)]
        public string LongName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceRaceDemographicDto obj)
        {
            return string.Compare(Code, obj.Code, StringComparison.Ordinal);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.API.Dtos
{
    public class ReferenceReligionDemographicDto : IComparable<ReferenceReligionDemographicDto>
    {
        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string DisplayText { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CompareTo(ReferenceReligionDemographicDto obj)
        {
            return String.Compare(this.Code, obj.Code, StringComparison.Ordinal);
        }
    }
}

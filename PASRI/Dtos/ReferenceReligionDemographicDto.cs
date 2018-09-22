using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.API.Dtos
{
    public class ReferenceReligionDemographicDto : IComparable<ReferenceReligionDemographicDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        [StringLength(253)]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceReligionDemographicDto obj)
        {
            return String.Compare(this.Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
